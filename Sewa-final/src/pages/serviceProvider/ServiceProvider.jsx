import React from 'react'
import { Route, Routes } from 'react-router-dom'
import Home from '../../components/serviceProvider/home/Home'
import NavBar from '../../components/serviceProvider/navbar/NavBar'
import styles from './ServiceProvider.module.scss'

const ServiceProvider = () => {
    return (
        <div className={styles.serviceprovider}>
            <div className={styles.navbar}>
                <NavBar />
            </div>
            <div className={styles.content}>
                <Routes>
                    <Route path="home" element={<Home />} />                  
                </Routes>
            </div>
        </div>
    )
}

export default ServiceProvider
