import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { toast } from 'react-toastify';
import { fetchAllCategories, generateToken } from '../../../redux/slice/tokenSlice';
import Loader from '../../loader/Loader';
import Card from '../../card/Card';
import styles from "./GenerateToken.module.scss";

const GenerateToken = () => {

    const dispatch = useDispatch();
    const loading = useSelector((state) => state.token.loading);
    const categories = useSelector((state) => state.token.categories);
    const token = useSelector((state) => state.token.token);

    const [serviceTypeId, setserviceType] = useState('');

    const handleDropdownChange = (event) => {
        // Update the selected value when the dropdown changes
        setserviceType(event.target.value);
    };

    const fetchCategories = async () => {
        try {
            await dispatch(fetchAllCategories());
        } catch (error) {
            toast.error(error.message);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, [])


    const handleClick = async (e) => {
        e.preventDefault();
        const response = await dispatch(generateToken({ serviceTypeId }));
        if (response.meta.requestStatus === 'fulfilled') {
            toast.success("Token Generated Successfully...");
        }
    };

    return (

        <>
            {loading && <Loader />}
            <h2> Add New Token</h2>
            <div className={styles.token}>
                <Card cardClass={styles.card}>
                    <form onSubmit={handleClick}>
                        <label>Service Category:</label>
                        <select
                            required
                            name="categoryId"
                            value={serviceTypeId}
                            onChange={(e) => handleDropdownChange(e)}
                        >
                            <option value="" disabled>
                                -- choose service category --
                            </option>
                            {categories.map((cat) => {
                                return (
                                    <option key={cat.id} value={cat.id}>
                                        {cat.name}
                                    </option>
                                );
                            })}
                        </select>
                        <button className="--btn --btn-primary">
                            Generate Token
                        </button>
                    </form>
                    <h1>{token}</h1>
                </Card>
            </div>
        </>
    )
}

export default GenerateToken
