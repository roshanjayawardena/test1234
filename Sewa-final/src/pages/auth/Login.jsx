import React, { useEffect, useState } from 'react'
import { Link, useNavigate } from "react-router-dom";
import styles from './Auth.module.scss'
import loginImg from '../../assets/login.png'
import { FaGoogle } from "react-icons/fa";
import Card from '../../components/card/Card';
import { useDispatch, useSelector } from 'react-redux';
import { login } from '../../redux/slice/authSlice';
import Loader from '../../components/loader/Loader';
import { toast } from 'react-toastify';

const Login = () => {

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const loading = useSelector((state) => state.auth.loading);
  const error = useSelector((state) => state.auth.error);
  const user = useSelector((state) => state.auth.user);

  useEffect(() => {
    if (user) {
      navigate("/");
    }
  }, [user, navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const response = await dispatch(login({ email, password }));
    if (response.meta.requestStatus === 'fulfilled') {
      toast.success("Login Successful...");     
    }
  };

  return (
    <>
      {loading && <Loader />}
      <section className={`container ${styles.auth}`}>
        <div className={styles.img}>
          <img src={loginImg} alt='Login' width='400' />
        </div>
        <Card>
          <div className={styles.form}>
            <h2>Login</h2>
            {error && <div>{error}</div>}
            <form onSubmit={handleSubmit}>
              <input
                type="text"
                placeholder="Email"
                required
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              <input
                type="password"
                placeholder="Password"
                required
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <button type="submit" className="--btn --btn-primary --btn-block">
                Login
              </button>
              <div className={styles.links}>
                <Link to="/reset">Reset Password</Link>
              </div>
              <p> -- or -- </p>
              <button type="submit" className="--btn --btn-danger --btn-block">
                <FaGoogle color="#fff" /> Login with Google
              </button>
              <span className={styles.register}>
                <p>Don't have an account?</p>
                <Link to="/register">Register</Link>
              </span>
            </form>
          </div>
        </Card>
      </section>
    </>
  )
}

export default Login
