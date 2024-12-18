export interface Device {
  id?: string;
  serialNumber: string;
  caption: string;
  deletedAt?: Date;
  userId?: string;
  minTemperature?: number;
  maxTemperature?: number;
  minHumidity?: number;
  maxHumidity?: number;
  lastMessageDate?: Date;
}

export interface AssignDeviceRequest {
  serialNumber?: string;
}

export interface UpdateDeviceRequest {
  serialNumber: string; 
  caption: string;
  minTemperature?: number;
  maxTemperature?: number;
  minHumidity?: number;
  maxHumidity?: number;
}

