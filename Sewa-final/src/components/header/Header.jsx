import React, { useEffect, useState } from 'react'
import styles from "./Header.module.scss";
import { Link, NavLink, useNavigate } from "react-router-dom";
import { FaTimes, FaUserCircle } from 'react-icons/fa'
import { HiOutlineMenuAlt3 } from "react-icons/hi";
import { useDispatch, useSelector } from 'react-redux';
import ShowOnLogin, { ShowOnLogout } from '../hiddenLink/hiddenLink';
import { logoutUser } from '../../redux/slice/authSlice';
import { AdminOnlyLink } from '../adminOnlyRoute/AdminOnlyRoute';
import { RecieptantOnlyLink } from '../recieptantOnlyRoute/RecieptantOnlyRoute';
import { ServiceProviderOnlyLink } from '../serviceProviderOnlyRoute/ServiceProviderOnlyRoute';

const logo = (<div className={styles.logo}>
  <Link to="/">
    <h2>
      Se<span>wa</span>.
    </h2>
  </Link>
</div>)

const activeLink = ({ isActive }) => (isActive ? `${styles.active}` : "");

const Header = () => {

  const user = useSelector((state) => state.auth.user);

  const dispatch = useDispatch();
  const navigate = useNavigate();  

  const [showMenu, setShowMenu] = useState(false);
  const [displayName, setdisplayName] = useState("");

  const toggleMenu = () => {
    setShowMenu(!showMenu)
  }

  const hideMenu = () => {
    setShowMenu(false)
  }

  const logout = async (e) => {
    e.preventDefault();
    dispatch(logoutUser());    
  };

  useEffect(() => {
    if (user) {
      const u1 = user.email.slice(0, -10);
      const uName = u1.charAt(0).toUpperCase() + u1.slice(1);
      setdisplayName(uName);
    } else {
      setdisplayName("");
      dispatch(logoutUser());
    }

  }, [dispatch, navigate, user]);
 

  return (
    <>    
      <header>
        <div className={styles.header}>
          {logo}
          <nav
            className={
              showMenu ? `${styles["show-nav"]}` : `${styles["hide-nav"]}`
            }
          >
            <div
              className={
                showMenu
                  ? `${styles["nav-wrapper"]} ${styles["show-nav-wrapper"]}`
                  : `${styles["nav-wrapper"]}`
              }
              onClick={hideMenu}
            ></div>

            <ul onClick={hideMenu}>
              <li className={styles["logo-mobile"]}>
                {logo}
                <FaTimes size={22} color="#fff" onClick={hideMenu} />
              </li>
              <li>
                <AdminOnlyLink>
                  <Link to="/admin/home">
                    <button className="--btn --btn-primary">Admin</button>
                  </Link>
                </AdminOnlyLink>
              </li>
              <li>
                <RecieptantOnlyLink>
                  <Link to="/recieptant/home">
                    <button className="--btn --btn-primary">Recieptant</button>
                  </Link>
                </RecieptantOnlyLink>
              </li>
              <li>
                <ServiceProviderOnlyLink>
                  <Link to="/serviceprovider/home">
                    <button className="--btn --btn-primary">Service Provider</button>
                  </Link>
                </ServiceProviderOnlyLink>
              </li>
              <li>
                <NavLink to="/" className={activeLink}>
                  Home
                </NavLink>
              </li>
              <li>
                <NavLink to="/contact" className={activeLink}>
                  Contact Us
                </NavLink>
              </li>
            </ul>
            <div className={styles["header-right"]} onClick={hideMenu}>
              <span className={styles.links}>
                <ShowOnLogout>
                  <NavLink to="/login" className={activeLink}>
                    Login
                  </NavLink>
                </ShowOnLogout>
                <ShowOnLogin>
                  <a href="#home" style={{ color: "#ff7722" }}>
                    <FaUserCircle size={16} />
                    Hi, {displayName}
                  </a>
                </ShowOnLogin>             
                <ShowOnLogin>
                  <NavLink to="/" onClick={logout}>
                    Logout
                  </NavLink>
                </ShowOnLogin>
              </span>
           
            </div>
          </nav>
          <div className={styles["menu-icon"]}>          
            <HiOutlineMenuAlt3 size={28} onClick={toggleMenu} />
          </div>
        </div>
      </header>
    </>
  )
}

export default Header
