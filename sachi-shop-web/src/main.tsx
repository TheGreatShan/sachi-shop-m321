import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import Router from './Router.tsx'


import { CartProvider } from "./context/CartContext";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <CartProvider>
      <Router />
    </CartProvider>
  </StrictMode>,
)
