import apiClient from './serviceHelper';
import { WrapResponse } from '../entities/common/wrapResponse';
import { Device, AssignDeviceRequest, UpdateDeviceRequest } from 'src/entities/device';

const deviceService = {
  myDevices: async (): Promise<WrapResponse<Device[]>> => {
    const response = await apiClient.get<WrapResponse<Device[]>>('/devices/mydevices');
    return response.data;
  },
  assign: async (data: AssignDeviceRequest): Promise<WrapResponse<boolean>> => {
    const response = await apiClient.post<WrapResponse<boolean>>('/devices/assign', data);
    return response.data;
  },
  update: async (data: UpdateDeviceRequest): Promise<WrapResponse<boolean>> => {
    const response = await apiClient.put<WrapResponse<boolean>>('/devices', data);
    return response.data;
  }
};

export default deviceService;