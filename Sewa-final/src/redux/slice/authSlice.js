import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import axiosInstance from '../../api/axiosService';
import jwtDecode from 'jwt-decode';


const initialState = {

    accessToken: localStorage.getItem('accessToken') || null,    
    isAuthenticated: localStorage.getItem('accessToken') ? true : false,
    loading: false,
    error: null,
    registerStatus: null,
    user: JSON.parse(localStorage.getItem('user')) ?? null
};

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {

        logoutUser: (state) => {
            state.isAuthenticated = false;
            state.user = null;
            localStorage.removeItem('accessToken');           
            localStorage.removeItem('user');
            state.accessToken = null;            
        }
    },
    extraReducers: (builder) => {
        builder.addCase(login.pending, (state) => {
            state.loading = true;
        })
        builder.addCase(login.fulfilled, (state, action) => {
            state.loading = false;
            if (action.payload.accessToken) {
                const decodedToken = jwtDecode(action.payload.accessToken);
                const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
                const email = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
                const id = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
                state.user = {
                    role: role,
                    email: email,
                    id: id,
                    userStatus: "Pending",
                }
                state.accessToken = action.payload.accessToken;                
                state.isAuthenticated = true;
                state.error = null;
                localStorage.setItem('user', JSON.stringify(state.user));
            } else return state;
        })
        builder.addCase(login.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload;
            state.user = null;
            state.isAuthenticated = false;
        })
    },
});


// Login action
export const login = createAsyncThunk(
    'auth/login',
    async ({ email, password }, { rejectWithValue }) => {

        try {
            const response = await axiosInstance.post('/api/Auth/login', { email, password });
            const { accessToken } = response.data;
            localStorage.setItem('accessToken', accessToken);           
            return response.data;
        } catch (error) {
            return rejectWithValue(error.response.data.detail);
        }
    }
);


export const { logoutUser } = authSlice.actions
export const isAuthenticated = (state) => state.auth.isAuthenticated;
export const userRole = (state) => state.auth.user.role;
export const userId = (state) => state.auth.user.id;

export default authSlice.reducer