import React from 'react';
import Button from '../../../components/ui/Button';
import Icon from '../../../components/AppIcon';
import CartQuotationsButton from './CartQuotationsButton';

const CartSummary = ({ 
  itemCount, cartItems = [], onRequestQuotation, isLoading 
}) => {
  return (
    <div className="bg-card border border-border rounded-lg p-6 sticky top-24">
      <h2 className="text-lg font-semibold text-foreground mb-4">
        Order Summary
      </h2>
      <div className="mb-6">
        <p className="text-sm font-medium text-foreground mb-2">
          {itemCount} {itemCount === 1 ? 'item' : 'items'} in your cart
        </p>
        <ul className="list-disc list-inside text-sm text-muted-foreground">
          {cartItems.map((item, idx) => (
            <li key={item.productId || item.id || idx}>
              <span className="text-foreground font-medium">{item.name}</span> &times; <span>{item.quantity}</span>
            </li>
          ))}
        </ul>
      </div>
      <Button
        variant="default"
        size="lg"
        fullWidth
        onClick={onRequestQuotation}
        loading={isLoading}
      >
        Request Quotation
      </Button>
      <div className="mt-4 text-xs text-muted-foreground">
        <Icon name="Info" size={16} className="inline-block mr-1 align-text-bottom" />
        Our team will review your request and provide a detailed quotation within 24 hours including bulk discounts and shipping options.
      </div>
      <CartQuotationsButton />
    </div>
  );
};

export default CartSummary;