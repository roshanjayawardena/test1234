





import React from 'react'
import NavBar from '../../components/recieptant/navbar/NavBar'
import { Route, Routes } from 'react-router-dom'
import styles from './Recieptant.module.scss'
import Home from '../../components/recieptant/home/Home'
import GenerateToken from '../../components/recieptant/generateToken/GenerateToken'

const Recieptant = () => {
    return (
        <div className={styles.recieptant}>
            <div className={styles.navbar}>
                <NavBar />
            </div>
            <div className={styles.content}>
                <Routes>
                    <Route path="home" element={<Home />} />
                    <Route path="generate-token" element={<GenerateToken />} />  
                </Routes>
            </div>
        </div>
    )
}

export default Recieptant












