import React, { useState } from 'react'
import { FaUserCircle } from 'react-icons/fa';
import { NavLink } from 'react-router-dom';
import styles from "./Navbar.module.scss";
import { useSelector } from 'react-redux';

const activeLink = ({ isActive }) => (isActive ? `${styles.active}` : "");

const Navbar = () => {
  const user = useSelector((state) => state.auth.user);

  return (
    <div className={styles.navbar}>
      <div className={styles.user}>
        <FaUserCircle size={40} color="#fff" />
        <h4>{user && user.email}</h4>
      </div>
      <nav>
        <ul>
          <li>
            <NavLink to="/admin/home" className={activeLink}>
              Home
            </NavLink>
          </li>          
        </ul>
      </nav>
    </div>
  );
};

export default Navbar;
