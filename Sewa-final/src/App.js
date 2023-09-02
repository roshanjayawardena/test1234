import { BrowserRouter, Route, Routes } from 'react-router-dom';
// Pages
import { Home, Contact, Login, Admin } from "./pages";
//components
import { Header, Footer } from "./components";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import AdminOnlyRoute from './components/adminOnlyRoute/AdminOnlyRoute';
import Recieptant from './pages/recieptant/Recieptant';
import RecieptantOnlyRoute from './components/recieptantOnlyRoute/RecieptantOnlyRoute';
import ServiceProviderOnlyRoute from './components/serviceProviderOnlyRoute/ServiceProviderOnlyRoute';
import ServiceProvider from './pages/serviceProvider/ServiceProvider';

function App() {
  return (
    <>
      <BrowserRouter>
        <ToastContainer />
        <Header />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/login" element={<Login />} />

          <Route
            path="/admin/*"
            element={
              <AdminOnlyRoute>
                <Admin />
              </AdminOnlyRoute>
            }
          />

          <Route
            path="/Recieptant/*"
            element={
              <RecieptantOnlyRoute>
                <Recieptant />
              </RecieptantOnlyRoute>
            }
          />

          <Route
            path="/ServiceProvider/*"
            element={
              <ServiceProviderOnlyRoute>
                <ServiceProvider />
              </ServiceProviderOnlyRoute>
            }
          />
        </Routes>
        <Footer />
      </BrowserRouter>
    </>
  );
}

export default App;
