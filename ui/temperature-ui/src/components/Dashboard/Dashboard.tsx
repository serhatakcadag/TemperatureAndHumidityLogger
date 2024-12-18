import React, { useEffect, useState } from "react";
import Dropdown from "./Dropdown/Dropdown";
import Button from "./Button/Button";
import Modal from "./Modal/Modal";
import DevicePreferences from "./DevicePreferences/DevicePreferences";
import Chart from "./Chart/Chart";
import styles from "./Dashboard.module.scss";
import { Device } from "src/entities/device";
import { deviceService } from "src/api";
import axios from "axios";

const Dashboard: React.FC = () => {
  const [devices, setDevices] = useState<Device[]>([]);
  const [selectedDevice, setSelectedDevice] = useState<
    Device | null | undefined
  >(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [serialNumber, setSerialNumber] = useState<string>("");
  const [responseMessage, setResponseMessage] = useState<string>("");

  const fetchDevice = async () => {
    try {
      const response = await deviceService.myDevices();
      setDevices(response.result);
    } catch (error) {
      console.error("Error fetching device:", error);
    }
  };

  useEffect(() => {
    fetchDevice();
  }, []);

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
                  datas={[1, 2, 3]}
                  xLabels={["11", "12", "13"]}
                />
              </div>
              <div className={styles.singleChart}>
                <Chart
                  chartLabel="Humidity"
                  color="blue"
                  datas={[1, 2, 3]}
                  xLabels={["11", "12", "13"]}
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
          {responseMessage && <p className={styles.response}>{responseMessage}</p>}
          <Button onClick={handleAssignDevice} label="Add" />
        </form>
      </Modal>
    </div>
  );
};

export default Dashboard;
