import { BrowserRouter, Routes, Route } from "react-router-dom";
import App from "./App";
import Profile from "./Profile";
import Cart from "./Cart";
import Navbar from "./components/Navbar";
import Dashboard from "./Dashboard";



function Router() {
  return (
      <BrowserRouter>
          <Routes>
            <Route path="/" element={<Navbar />}>
              <Route index element={<App/>} />
              <Route path="profile" element={<Profile />} />
              <Route path="cart" element={<Cart />} />
              <Route path="dashboard" element={<Dashboard />} />
            </Route>
          </Routes>
      </BrowserRouter>
  );
}

export default Router;
