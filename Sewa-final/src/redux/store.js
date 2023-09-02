import { configureStore, combineReducers } from "@reduxjs/toolkit"
import authReducer from "./slice/authSlice";
import tokenReducer from "./slice/tokenSlice";

const rootReducer = combineReducers({
    auth: authReducer,  
    token: tokenReducer
});

const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
            serializableCheck: false,
        }),
});

export default store

