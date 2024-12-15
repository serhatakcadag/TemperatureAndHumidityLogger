import React, { useState } from 'react';
import axios from 'axios';
import Styles from './Login.module.scss';
import userService from '../../api/userService';
import { setAuthorizationHeader } from 'src/api/serviceHelper';
import { useNavigate } from "react-router-dom";

const Login: React.FC = () => {
  const navigate = useNavigate();

  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [responseMessage, setResponseMessage] = useState({
    message: '',
    success: false
  });
  const [errors, setErrors] = useState({
    email: '',
    password: '',
  });

  const validateForm = (): boolean => {
    const newErrors = {
      email: '',
      password: '',
    };

    if (!email) {
      newErrors.email = 'Email is required.';
    } else if (!/\S+@\S+\.\S+/.test(email)) {
      newErrors.email = 'Email is not valid.';
    }

    if (!password) {
      newErrors.password = 'Password is required.';
    }

    setErrors(newErrors);

    // Return true if there are no errors
    return !Object.values(newErrors).some((error) => error !== '');
  };

  const handleLogin = async () => {
    if (!validateForm()) {
      return;
    }

    try {
      const response = await userService.login({ email, password });
      const message = {
        message: response.message ?? 'Login successful!',
        success: true
      }
      setResponseMessage(message);

      localStorage.setItem('authToken', response.result);

      setAuthorizationHeader(response.result);

      navigate('/');
    } catch (error) {
        const message = {
            message: 'Login failed. Please try again.',
            success: false
        }
        if (axios.isAxiosError(error) && error.response?.data?.message) {
            setResponseMessage({...message, message: error.response.data.message ?? 'Login failed. Please try again.'});
        } else {
            setResponseMessage(message);
        }
    }
  };

  return (
    <div className={Styles.loginContainer}>
      <h2>Login</h2>
      <form onSubmit={(e) => e.preventDefault()} className={Styles.form}>
        <div className={Styles.inputGroup}>
          <label htmlFor="email">Email</label>
          <input
            type="text"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          {errors.email && <p className={Styles.error}>{errors.email}</p>}
        </div>
        <div className={Styles.inputGroup}>
          <label htmlFor="password">Password</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          {errors.password && <p className={Styles.error}>{errors.password}</p>}
        </div>
        <button type="button" className={Styles.loginButton} onClick={handleLogin}>
          Login
        </button>
      </form>
      {responseMessage.message && <p className={responseMessage.success ? Styles.response : Styles.error}>{responseMessage.message}</p>}
      <p className={Styles.redirect}>
        Don't have an account?{' '}
        <span onClick={() => navigate('/register')} className={Styles.link}>
          Sign up here
        </span>
      </p>
    </div>
  );
};

export default Login;