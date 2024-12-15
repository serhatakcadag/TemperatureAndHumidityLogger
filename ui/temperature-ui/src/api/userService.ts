import apiClient from './serviceHelper';
import { UserLoginRequest, UserRegisterRequest } from '../entities/user';
import { WrapResponse } from '../entities/common/wrapResponse';

const userService = {
  register: async (data: UserRegisterRequest): Promise<WrapResponse<string>> => {
    const response = await apiClient.post<WrapResponse<string>>('/users/register', data);
    return response.data;
  },
  login: async (data: UserLoginRequest): Promise<WrapResponse<string>> => {
    const response = await apiClient.post<WrapResponse<string>>('/users/login', data);
    return response.data;
  },
};

export default userService;