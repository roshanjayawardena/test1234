import { useSelector } from "react-redux";
import { isAuthenticated } from "../../redux/slice/authSlice";

const ShowOnLogin = ({ children }) => {
    const isLoggedIn = useSelector(isAuthenticated);

    if (isLoggedIn) {
        return children;
    }
    return null;
};

export const ShowOnLogout = ({ children }) => {
    const isLoggedIn = useSelector(isAuthenticated);

    if (!isLoggedIn) {
        return children;
    }
    return null;
};

export default ShowOnLogin;