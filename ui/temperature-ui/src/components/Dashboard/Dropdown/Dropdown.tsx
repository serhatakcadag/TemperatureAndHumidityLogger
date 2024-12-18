import React from 'react';
import styles from './Dropdown.module.scss';
import { Device } from 'src/entities/device';

interface DropdownProps {
  devices: Device[];
  onSelect: (device: string) => void;
}

const Dropdown: React.FC<DropdownProps> = ({ devices, onSelect }) => {
  return (
    <select style={{ margin: '0 auto' }} className={styles.dropdown} onChange={(e) => onSelect(e.target.value)}>
      <option value="" disabled selected>
        Select a device
      </option>
      {devices.map((device, index) => (
        <option key={index} value={device.id}>
          {device.caption || device.serialNumber || ""}
        </option>
      ))}
    </select>
  );
};

export default Dropdown;