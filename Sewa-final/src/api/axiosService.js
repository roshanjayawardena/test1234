import axios from 'axios';
import { toast } from 'react-toastify';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7177',
  timeout: 60000,
  headers: {
    'Content-Type': 'application/json',
  },
});

axiosInstance.interceptors.request.use((config) => {

  // Add the access token to the request headers
  const accessToken = localStorage.getItem('accessToken');

  if (accessToken) {
    config.headers['Authorization'] = `Bearer ${accessToken}`;
  }
  return config;
},
  (error) => {
    return Promise.reject(error);
  });

// Error interceptor
axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {


    if (error.response && error.response.data !== "") {
      // The request was made and the server responded with an error status code
      const errorObject = error.response.data;
      if (errorObject.status === 400 && Array.isArray(errorObject.errors)) {

        Object.keys(errorObject.errors).forEach((key) => {
          const errorMessage = errorObject.errors[key][0]; // Assuming there's only one error message per field
          toast.error(errorMessage);
        });
      } else {
        toast.error(errorObject.detail);
      }

    } else if (error.response && error.response.data === "" && error.response.status === 401) {

      localStorage.removeItem('accessToken');
      // Redirect to the login page or any other desired action
      window.location.href = '/login'; // Replace with your login page URL

    } else if (error.message) {
      toast.error(error.message);
      // The request was made, but no response was received (e.g., network error)     
      // Handle network errors as required
    } else {
      // Something happened in setting up the request and triggered an error     
      //toast.error(error.message);
      // Handle other errors as required
    }

    // You can also throw the error to be caught in the component that initiated the request
    return Promise.reject(error);
  }
);

export default axiosInstance;   