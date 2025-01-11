import { useState } from "react"
import { Link, Outlet } from "react-router-dom";

import { useCart } from "../context/CartContext";

export default function Navbar() {
  const [showProfile, setShowProfile] = useState(false)
  const [showCart, setShowCart] = useState(false)

  const { cart } = useCart()

  const totalCount = cart.reduce((sum, item) => sum + item.count, 0);

  const toggleProfile = () => {
    setShowProfile(!showProfile)
  }

  const toggleCart = () => {
    setShowCart(!showCart)
  }

  return (
    <div>
      <div className="p-5 bg-blue-500 text-white grid grid-cols-6 fixed top-0 left-0 w-full z-10 items-center">
        <Link to="/">
          <div className="col-span-1 flex gap-5">
            <img src="/shop-logo.svg" alt="logo" height={40} width={40} />
            <span className="text-xl content-center">Sachi Shop</span>
          </div>
        </Link>
        <div className="col-span-4"></div>
        <div className="col-span-1 justify-self-end grid grid-cols-2 gap-7">
          <Link to="/cart">
            <div onMouseEnter={toggleCart} onMouseLeave={toggleCart} className="cursor-pointer">
              <img src="/cart.svg" alt="profile" height={38} width={38} />
              {
                totalCount > 0 &&
                  <span className="absolute top-10 right-19 bg-red-600 text-white text-xs font-bold px-1.5 py-0.5 rounded-full">
                    {
                      totalCount > 9 ? "9+" : totalCount
                    }
                  </span>
              }
            </div>
          </Link>
          <Link to="/profile">
            <div onMouseEnter={toggleProfile} onMouseLeave={toggleProfile} className="cursor-pointer">
              <img src="/profile.svg" alt="profile" height={34} width={34} />
            </div>
          </Link>
        </div>
      </div>
      {
        showProfile &&
        <div className="bg-gray-100 py-1.5 px-3 fixed right-2 shadow-lg rounded-lg z-50 top-16">
          <p className="text-sm">Profile</p>
        </div>
      }
      
      {
        showCart &&
        <div className="bg-gray-100 py-1.5 px-3 fixed right-20 shadow-lg rounded-lg z-50 top-16">
          <p className="text-sm">Cart</p>
        </div>
      }
      <Outlet />
    </div>
  );
}
