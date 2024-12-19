import apiClient from './serviceHelper';
import { WrapResponse } from '../entities/common/wrapResponse';
import { LogResponse } from 'src/entities/log';

const logService = {
  logsByDeviceId: async (deviceId : string): Promise<WrapResponse<LogResponse[]>> => {
    const response = await apiClient.get<WrapResponse<LogResponse[]>>(`/logs/${deviceId}`);
    return response.data;
  }
};

export default logService;