import React from 'react';
import styles from './Footer.module.scss';

const Footer : React.FC = () => {
  return (
    <footer className={styles.footer}>
      <p style={{ margin: 0 }}>© 2024-2025 Kocaeli University Final Project. All Rights Reserved.</p>
    </footer>
  );
};

export default Footer;