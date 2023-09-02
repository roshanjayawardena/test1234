import React from 'react'
import styles from './Footer.module.scss'

const footer = () => {

  const date = new Date();
  const year = date.getFullYear();

  return (
    <div className={styles.footer}>
     &copy; {year} All Rights reserved.
    </div>
  )
}

export default footer
