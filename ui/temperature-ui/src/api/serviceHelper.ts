import axios from 'axios';

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL;

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const setAuthorizationHeader = (token: string | null) => {
    if (token) {
        apiClient.defaults.headers.Authorization = `Bearer ${token}`;
    } else {
        delete apiClient.defaults.headers.Authorization;
    }
};

apiClient.interceptors.response.use(
  (response) => {return response},
  (error) => {
    if(error.response)
    {
      const statusCode = error.response.status;
      const invalidToken = error.response.headers["WWW-Authenticate"]?.includes('invalid-token');

      if(statusCode === 401 && invalidToken)
      {
        localStorage.removeItem('authToken');
        setAuthorizationHeader(null);

        window.location.href = '/login';
      }
    }

    return Promise.reject(error);
  }
)


export default apiClient;
