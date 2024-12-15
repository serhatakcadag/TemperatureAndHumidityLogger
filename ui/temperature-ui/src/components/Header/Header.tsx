import React from "react";
import { setAuthorizationHeader } from "src/api/serviceHelper";
import { useNavigate } from "react-router-dom";
import styles from './Header.module.scss';
import {FiLogOut} from 'react-icons/fi';

const Header: React.FC = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("authToken");
    setAuthorizationHeader(null);

    navigate("/login");
  };

  return (
    <header className={styles.header}>
      <h1 className={styles.title}>Dashboard</h1>
      <button className={styles.logoutButton} onClick={handleLogout}>
        <div className={styles.logoutContainer}>
            <FiLogOut className={styles.icon}/>
            <div>Log Out</div>
        </div>
      </button>
    </header>
  );
};

export default Header;