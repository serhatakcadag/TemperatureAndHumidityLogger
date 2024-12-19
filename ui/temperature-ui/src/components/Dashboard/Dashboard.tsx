import React, { useEffect, useState } from "react";
import Dropdown from "./Dropdown/Dropdown";
import Button from "./Button/Button";
import Modal from "./Modal/Modal";
import DevicePreferences from "./DevicePreferences/DevicePreferences";
import Chart from "./Chart/Chart";
import styles from "./Dashboard.module.scss";
import { Device } from "src/entities/device";
import { deviceService, logService } from "src/api";
import axios from "axios";
import { LogResponse } from "src/entities/log";
import dayjs from "dayjs";

const Dashboard: React.FC = () => {
  const [devices, setDevices] = useState<Device[]>([]);
  const [logs, setLogs] = useState<LogResponse[]>([]);
  const [selectedDevice, setSelectedDevice] = useState<
    Device | null | undefined
  >(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [serialNumber, setSerialNumber] = useState<string>("");
  const [responseMessage, setResponseMessage] = useState<string>("");
  const [temperatureChartData, setTemperatureChartData] = useState<{
    xLabels: any[];
    yValues: (number | null)[];
  }>({
    xLabels: [],
    yValues: [],
  });
  const [humidityChartData, setHumidityChartData] = useState<{
    xLabels: any[];
    yValues: (number | null)[];
  }>({
    xLabels: [],
    yValues: [],
  });

  const fetchDevice = async () => {
    try {
      const response = await deviceService.myDevices();
      setDevices(response.result);
    } catch (error) {
      console.error("Error fetching device:", error);
    }
  };

  const fetchLogs = async () => {
    try {
      if (!selectedDevice?.id) {
        return;
      }
      const response = await logService.logsByDeviceId(selectedDevice?.id);
      setLogs(response.result);
    } catch (error) {
      console.error("Error fetching device:", error);
    }
  };

  useEffect(() => {
    fetchDevice();
  }, []);

  useEffect(() => {
    fetchLogs();
  }, [selectedDevice]);

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  const handleSelectDevice = (deviceId: string) => {
    var device = devices.find((d) => d.id === deviceId);
    setSelectedDevice(device);
  };

  const handleAssignDevice = async () => {
    try {
      const response = await deviceService.assign({ serialNumber });
      if (response.result) {
        await fetchDevice();
        setResponseMessage("Device assigned successfully");
      }
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.data?.message) {
        setResponseMessage(
          error.response.data.message ??
            "Assign operation has failed. Please try again."
        );
      } else {
        setResponseMessage("Assign operation has failed. Please try again.");
      }
    }
  };

  useEffect(() => {
    const intervalInMinutes = 15; // 1 saatlik aralık
    const temperatureProcessedData = processChartData(
      logs,
      intervalInMinutes,
      "temperature"
    );
    const humidityProcessedData = processChartData(
      logs,
      intervalInMinutes,
      "humidity"
    );
    setTemperatureChartData(temperatureProcessedData);
    setHumidityChartData(humidityProcessedData);
  }, [logs]);

  function processChartData(
    logs: Array<LogResponse>,
    intervalInMinutes: number,
    dataKey: string
  ) {
    const now = dayjs();
    const startTime = now.subtract(24, "hour");

    const timeIntervals: Array<any> = [];
    let currentTime = startTime;
    while (currentTime.isBefore(now)) {
      timeIntervals.push(currentTime);
      currentTime = currentTime.add(intervalInMinutes, "minute");
    }

    const data = timeIntervals.map((start, index) => {
      const end = timeIntervals[index + 1] || now;

      const logsInInterval = logs.filter((log) => {
        const logTime = dayjs(log.createdAt);
        return logTime.isAfter(start) && logTime.isBefore(end);
      });

      // Ortalamasını al
      const avgValue =
        //@ts-ignore
        logsInInterval.reduce((sum, log) => sum + log[dataKey], 0) /
        (logsInInterval.length || 1);

      return {
        label: start.format("HH:mm"), // xLabel
        value: logsInInterval.length > 0 ? avgValue : null, // yValue
      };
    });

    // Etiketler ve veriler
    const xLabels = data.map((item) => item.label);
    const yValues = data.map((item) => item.value);

    return { xLabels, yValues };
  }

  return (
    <div className={styles.dashboard}>
      <div className={styles.topBar}>
        <Dropdown devices={devices} onSelect={handleSelectDevice} />
        <Button onClick={handleOpenModal} label="Add Device" />
      </div>

      <div className={styles.content}>
        {selectedDevice && (
          <DevicePreferences
            initialDevice={selectedDevice}
            setInitialDevice={setSelectedDevice}
            setDevices={setDevices}
            devices={devices}
          />
        )}
        <div className={styles.chartContainer}>
          {selectedDevice ? (
            <div className={styles.content}>
              <div className={styles.singleChart}>
                <Chart
                  chartLabel="Temperature"
                  datas={temperatureChartData.yValues}
                  xLabels={temperatureChartData.xLabels}
                />
              </div>
              <div className={styles.singleChart}>
                <Chart
                  chartLabel="Humidity"
                  color="blue"
                  datas={humidityChartData.yValues}
                  xLabels={humidityChartData.xLabels}
                />
              </div>
            </div>
          ) : (
            <p>Please select a device.</p>
          )}
        </div>
      </div>

      <Modal isOpen={isModalOpen} onClose={handleCloseModal}>
        <h3>Add Device</h3>
        <form onSubmit={(e) => e.preventDefault()} className={styles.form}>
          <div style={{ marginBottom: "10px" }} className={styles.inputGroup}>
            <label htmlFor="serialNumber">Serial Number</label>
            <input
              type="text"
              id="serialNumber"
              value={serialNumber}
              onChange={(e) => setSerialNumber(e.target.value)}
            />
          </div>
          {responseMessage && (
            <p className={styles.response}>{responseMessage}</p>
          )}
          <Button onClick={handleAssignDevice} label="Add" />
        </form>
      </Modal>
    </div>
  );
};

export default Dashboard;
