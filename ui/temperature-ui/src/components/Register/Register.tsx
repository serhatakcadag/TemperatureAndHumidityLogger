import React, { useState } from 'react';
import {useNavigate} from 'react-router-dom'
import userService from '../../api/userService';
import { UserRegisterRequest } from '../../entities/user';
import Styles from './Register.module.scss';
import axios from 'axios';


const Register: React.FC = () => {
  const navigate = useNavigate();

  const [username, setUsername] = useState('');
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [responseMessage, setResponseMessage] = useState<string | null>(null);
  const [errors, setErrors] = useState({
    username: '',
    email: '',
    phoneNumber: '',
    password: '',
    confirmPassword: '',
  });

  const validateForm = (): boolean => {
    const newErrors: typeof errors = {
      username: '',
      email: '',
      phoneNumber: '',
      password: '',
      confirmPassword: '',
    };

    if (!username) {
      newErrors.username = 'Username is required.';
    } else if (username.length < 3) {
      newErrors.username = 'Username must be at least 3 characters.';
    }

    if (!email) {
      newErrors.email = 'Email is required.';
    } else if (!/\S+@\S+\.\S+/.test(email)) {
      newErrors.email = 'Email is not valid.';
    }

    if (!phoneNumber) {
      newErrors.phoneNumber = 'Phone number is required.';
    } else if (!/^\d{10}$/.test(phoneNumber)) {
      newErrors.phoneNumber = 'Phone number must be 10 digits.';
    }

    if (!password) {
      newErrors.password = 'Password is required.';
    } else if (password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters.';
    }

    if (confirmPassword !== password) {
      newErrors.confirmPassword = 'Passwords do not match.';
    }

    setErrors(newErrors);

    // Return true if no errors exist
    return !Object.values(newErrors).some((error) => error !== '');
  };

  const handleRegister = async () => {
    if (!validateForm()) {
      return;
    }

    const requestData: UserRegisterRequest = {
      username,
      password,
      phoneNumber,
      email,
    };

    try {
      const response = await userService.register(requestData);
      setResponseMessage(response.message);
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.data?.message) {
        setResponseMessage(error.response.data.message ?? 'Registration failed. Please try again.');
      } else {
        setResponseMessage('Registration failed. Please try again.');
      }
    }
  };

  return (
    <div className={Styles.registerContainer}>
      <h2>Register</h2>
      <form onSubmit={(e) => e.preventDefault()} className={Styles.form}>
        <div className={Styles.inputGroup}>
          <label htmlFor="username">Username</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          {errors.username && <p className={Styles.error}>{errors.username}</p>}
        </div>
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
          <label htmlFor="phoneNumber">Phone Number</label>
          <input
            type="text"
            id="phoneNumber"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
          />
          {errors.phoneNumber && <p className={Styles.error}>{errors.phoneNumber}</p>}
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
        <div className={Styles.inputGroup}>
          <label htmlFor="passwordConfirmation">Password Confirmation</label>
          <input
            type="password"
            id="passwordConfirmation"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
          {errors.confirmPassword && <p className={Styles.error}>{errors.confirmPassword}</p>}
        </div>
        <button type="button" className={Styles.registerButton} onClick={handleRegister}>
          Register
        </button>
      </form>
      {responseMessage && <p className={Styles.response}>{responseMessage}</p>}
      <p className={Styles.redirect}>
        Already have an account?{' '}
        <span onClick={() => navigate('/login')} className={Styles.link}>
          Log in here
        </span>
      </p>
    </div>
  );
};

export default Register;