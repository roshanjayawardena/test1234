import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import axiosInstance from '../../api/axiosService';

const initialState = {
    token: null,
    loading: false,
    categories: [],
    tickets: []
}

const tokenSlice = createSlice({
    name: "token",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchAllCategories.pending, (state, action) => {
            state.loading = true;
        });
        builder.addCase(fetchAllCategories.fulfilled, (state, action) => {
            state.loading = false;
            state.categories = action.payload;
        });
        builder.addCase(fetchAllCategories.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload;
        });

        builder.addCase(fetchAllTicketsByServiceTypeId.pending, (state, action) => {
            state.loading = true;
        });
        builder.addCase(fetchAllTicketsByServiceTypeId.fulfilled, (state, action) => {
            state.loading = false;
            state.tickets = action.payload;
        });
        builder.addCase(fetchAllTicketsByServiceTypeId.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload;
        });

        builder.addCase(fetchAllTicketsByServiceTypeIdAndStatus.pending, (state, action) => {
            state.loading = true;
        });
        builder.addCase(fetchAllTicketsByServiceTypeIdAndStatus.fulfilled, (state, action) => {
            state.loading = false;
            state.tickets = action.payload;
        });
        builder.addCase(fetchAllTicketsByServiceTypeIdAndStatus.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload;
        });

        builder.addCase(generateToken.pending, (state, action) => {
            state.loading = true;
        });
        builder.addCase(generateToken.fulfilled, (state, action) => {
            state.loading = false;
            state.token = action.payload.token

        });
        builder.addCase(generateToken.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload;
        });

        builder.addCase(updateTicketStatus.pending, (state, action) => {
            state.loading = true;
        });
        builder.addCase(updateTicketStatus.fulfilled, (state, action) => {
            state.loading = false;
        });
        builder.addCase(updateTicketStatus.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload;
        });

        builder.addCase(updateTicket.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(updateTicket.fulfilled, (state, action) => {
            state.loading = false;
        });
        builder.addCase(updateTicket.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload;
        });
    },
});

export const fetchAllCategories = createAsyncThunk(
    "category/getAPI",
    async () => {
        const response = await axiosInstance.get("/api/ServiceProvider");
        return response.data;
    });

export const generateToken = createAsyncThunk(
    'tickets/generateTicket',
    async ({ serviceTypeId }, { rejectWithValue }) => {

        try {
            const response = await axiosInstance.post('/api/Ticket', { serviceTypeId });
            return response.data;
        } catch (error) {
            return rejectWithValue(error.response.data.detail);
        }
    }
);

export const fetchAllTicketsByServiceTypeId = createAsyncThunk(
    "tickets/getAllTicketsByServiceTypeIdAPI",
    async ({ pageSize, pageNo, serviceTypeId }) => {
        debugger
        const response = await axiosInstance.get(`/api/Ticket/${serviceTypeId}/?pageSize=${pageSize}&pageIndex=${pageNo}`);
        return response.data;
    });

export const fetchAllTicketsByServiceTypeIdAndStatus = createAsyncThunk(
    "tickets/fetchAllTicketsByServiceTypeIdAndStatus",
    async ({ pageSize, pageNo, serviceTypeId, status }) => {
        debugger
        const response = await axiosInstance.get(`/api/Ticket/${serviceTypeId}/${status}/?pageSize=${pageSize}&pageIndex=${pageNo}`);
        return response.data;
    });

export const updateTicketStatus = createAsyncThunk(
    "tickets/updateTicketStatus",
    async (payload) => {
        const response = await axiosInstance.put(`/api/Ticket/${payload.ticketId}`, payload);
        return response.data;
    });

export const updateTicket = createAsyncThunk(
    "tickets/updateTicket",
    async (payload) => {
        const response = await axiosInstance.put(`/api/Ticket`, payload);
        return response.data;
    });

export const { } = tokenSlice.actions
export const allTickets = (state) => state.token.tickets;
export default tokenSlice.reducer