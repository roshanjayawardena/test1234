import React from 'react'
import styles from './Admin.module.scss'
import NavBar from '../../components/admin/navbar/NavBar';
import { Route, Routes } from 'react-router-dom';
import Home from '../../components/admin/home/Home';

const Admin = () => {
  return (
    <div className={styles.admin}>
      <div className={styles.navbar}>
        <NavBar />
      </div>
      <div className={styles.content}>
        <Routes>
          <Route path="home" element={<Home />} />
          {/* <Route path="all-products" element={<ViewProducts />} />    */}
        </Routes>
      </div>
    </div>
  );
}

export default Admin
