import React from "react";
import { BrowserRouter, Routes as RouterRoutes, Route } from "react-router-dom";
import ScrollToTop from "components/ScrollToTop";
import ErrorBoundary from "components/ErrorBoundary";
// Add your imports here
import LoginScreen from "pages/login-screen";
import RegistrationScreen from "pages/registration-screen";
import ProductCatalog from "pages/product-catalog";
import ProductDetail from "pages/product-detail";
import ShoppingCart from "pages/shopping-cart";
import CartQuotationsPage from "pages/cart-quotations";
import HomePage from "pages/home-page";
import CustomerDashboard from "pages/customer-dashboard";
import NotFound from "pages/NotFound";

const Routes = () => {
  return (
    <BrowserRouter>
      <ErrorBoundary>
      <ScrollToTop />
      <RouterRoutes>
        {/* Define your routes here */}
        <Route path="/" element={<HomePage />} />
        <Route path="/login-screen" element={<LoginScreen />} />
        <Route path="/registration-screen" element={<RegistrationScreen />} />
        <Route path="/product-catalog" element={<ProductCatalog />} />
        <Route path="/product-detail" element={<ProductDetail />} />
        <Route path="/shopping-cart" element={<ShoppingCart />} />
        <Route path="/cart-quotations" element={<CartQuotationsPage />} />
        <Route path="/customer-dashboard" element={<CustomerDashboard />} />
        <Route path="/home-page" element={<HomePage />} />
        <Route path="*" element={<NotFound />} />
      </RouterRoutes>
      </ErrorBoundary>
    </BrowserRouter>
  );
};

export default Routes;