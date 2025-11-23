import React from 'react';
import { Link } from 'react-router-dom';

const CartQuotationsButton = () => (
  <div className="mt-4 text-center">
    <Link to="/cart-quotations">
      <button className="bg-primary text-primary-foreground px-4 py-2 rounded-lg font-semibold shadow hover:bg-primary/90 transition border border-primary">
        View Final Quotations
      </button>
    </Link>
  </div>
);

export default CartQuotationsButton;
