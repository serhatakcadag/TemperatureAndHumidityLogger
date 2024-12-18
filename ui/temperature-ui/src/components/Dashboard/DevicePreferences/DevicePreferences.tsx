import React, { useEffect, useState } from "react";
import styles from "./DevicePreferences.module.scss";
import Button from "../Button/Button";
import { Device, UpdateDeviceRequest } from "src/entities/device";
import { deviceService } from "src/api";
import axios from "axios";

interface DevicePreferencesProps {
  initialDevice: Device;
  setInitialDevice: React.Dispatch<
    React.SetStateAction<Device | null | undefined>
  >;
  devices: Device[];
  setDevices: React.Dispatch<React.SetStateAction<Device[]>>;
}

const DevicePreferences: React.FC<DevicePreferencesProps> = ({
  initialDevice,
  setInitialDevice,
  devices,
  setDevices,
}) => {
  const [device, setDevice] = useState<Device>(initialDevice);
  const [responseMessage, setResponseMessage] = useState<string | null>(null);

  useEffect(() => {
    setDevice(initialDevice);
  }, [initialDevice]);

  const handleSubmit = async () => {
    const requestData: UpdateDeviceRequest = {
      serialNumber: device.serialNumber,
      caption: device.caption,
      maxHumidity: device.maxHumidity,
      minHumidity: device.minHumidity,
      maxTemperature: device.maxTemperature,
      minTemperature: device.minTemperature,
    };

    try {
      const response = await deviceService.update(requestData);
      setInitialDevice({ ...initialDevice, ...requestData });
      setDevices(
        devices.map((d) =>
          d.serialNumber === device.serialNumber ? { ...d, ...device } : d
        )
      );
      setResponseMessage("The device has updated successfully.");
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.data?.message) {
        setResponseMessage(
          error.response.data.message ?? "Update failed. Please try again."
        );
      } else {
        setResponseMessage("Update failed. Please try again.");
      }
    }
  };

  return (
    <aside className={styles.preferences}>
      <h3>{initialDevice.caption || initialDevice.serialNumber} Options</h3>
      <form
        onSubmit={(e) => e.preventDefault()}
        style={{ marginBottom: "10px" }}
        className={styles.form}
      >
        <div className={styles.inputGroup}>
          <label htmlFor="caption">Caption</label>
          <input
            type="text"
            id="caption"
            value={device.caption || ""}
            onChange={(e) => setDevice({ ...device, caption: e.target.value })}
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="minTemperature">Min Temperature</label>
          <input
            type="string"
            id="minTemperature"
            value={device.minTemperature || ""}
            onChange={(e) =>
              setDevice({ ...device, minTemperature: Number(e.target.value) })
            }
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="maxTemperature">Max Temperature</label>
          <input
            type="string"
            id="maxTemperature"
            value={device.maxTemperature || ""}
            onChange={(e) =>
              setDevice({ ...device, maxTemperature: Number(e.target.value) })
            }
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="minHumidity">Min Humidity</label>
          <input
            type="string"
            id="minHumidity"
            value={device.minHumidity || ""}
            onChange={(e) =>
              setDevice({ ...device, minHumidity: Number(e.target.value) })
            }
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="maxHumidity">Max Humidity</label>
          <input
            type="string"
            id="maxHumidity"
            value={device.maxHumidity || ""}
            onChange={(e) =>
              setDevice({ ...device, maxHumidity: Number(e.target.value) })
            }
          />
        </div>
      </form>
      {responseMessage && <p className={styles.response}>{responseMessage}</p>}
      <Button label="Update" onClick={handleSubmit} />
    </aside>
  );
};

export default DevicePreferences;
