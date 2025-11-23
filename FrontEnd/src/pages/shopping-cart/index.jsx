import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import Header from '../../components/ui/Header';
import CartItem from './components/CartItem';
import CartSummary from './components/CartSummary';
import EmptyCart from './components/EmptyCart';
import SavedForLater from './components/SavedForLater';
import RecentlyViewed from './components/RecentlyViewed';
import Button from '../../components/ui/Button';
import { fetchProductById } from '../../utils/api';
import { requestQuotation } from '../../utils/quotation';

const ShoppingCart = () => {
  // Clear cart handler
  const handleClearCart = () => {
    setCartItems([]);
    localStorage.removeItem('cartItems');
  };
  const navigate = useNavigate();
  const [cartItems, setCartItems] = useState(() => {
    const stored = localStorage.getItem('cartItems');
    return stored ? JSON.parse(stored) : [];
  });

  // Listen for localStorage changes (e.g., from ProductCatalog)
  React.useEffect(() => {
    const syncCart = () => {
      const stored = localStorage.getItem('cartItems');
      setCartItems(stored ? JSON.parse(stored) : []);
    };
    window.addEventListener('storage', syncCart);
    return () => window.removeEventListener('storage', syncCart);
  }, []);
  const [savedItems, setSavedItems] = useState([]);
  const [recentlyViewed, setRecentlyViewed] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [showSuccess, setShowSuccess] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');

  const handleUpdateQuantity = async (itemId, newQuantity) => {
    // Always fetch latest product data to ensure stock is present and up-to-date
    let latestProduct;
    try {
      latestProduct = await fetchProductById(itemId);
    } catch (err) {
      // fallback: do not update if fetch fails
      return;
    }
    setCartItems(items => {
      const updated = items.map(item => {
        if ((item.productId || item.id) === itemId) {
          // Merge latest product data (including stock) and update quantity
          return { ...item, ...latestProduct, quantity: newQuantity };
        }
        return item;
      });
      localStorage.setItem('cartItems', JSON.stringify(updated));
      return updated;
    });
  };

  const handleRemoveItem = (itemId) => {
    setCartItems(items => {
      const updated = items.filter(item => (item.productId || item.id) !== itemId);
      localStorage.setItem('cartItems', JSON.stringify(updated));
      return updated;
    });
  };

  const handleSaveForLater = (itemId) => {
    const item = cartItems.find(item => (item.productId || item.id) === itemId);
    if (item) {
      setSavedItems(prev => [...prev, { ...item, quantity: 1 }]);
      setCartItems(items => items.filter(item => (item.productId || item.id) !== itemId));
    }
  };

  const handleMoveToCart = (itemId) => {
    const item = savedItems.find(item => item.id === itemId);
    if (item) {
      setCartItems(prev => [...prev, { ...item, quantity: 1 }]);
      setSavedItems(items => items.filter(item => item.id !== itemId));
    }
  };

  const handleRemoveSaved = (itemId) => {
    setSavedItems(items => items.filter(item => item.id !== itemId));
  };

  const handleAddToCart = async (itemOrId) => {
    let product = itemOrId;
    if (typeof itemOrId === 'number' || typeof itemOrId === 'string') {
      try {
        product = await fetchProductById(itemOrId);
      } catch (err) {
        console.error('Failed to fetch product for cart:', err);
        return;
      }
    }
    setCartItems(prev => {
      const existingItem = prev.find(cartItem => cartItem.id === product.id);
      let updated;
      if (existingItem) {
        updated = prev.map(item =>
          item.id === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        );
      } else {
        updated = [...prev, { ...product, quantity: 1 }];
      }
      localStorage.setItem('cartItems', JSON.stringify(updated));
      return updated;
    });
  };

  const handleRequestQuotation = async () => {
    setIsLoading(true);
    try {
      // Get user info from localStorage
      const user = JSON.parse(localStorage.getItem('user'));
      if (!user || !user.userId) {
        throw new Error('User not logged in. Please log in to request a quotation.');
      }
      // Prepare productRequests for API
      const productRequests = cartItems.map(item => ({
        productId: String(item.productId || item.id),
        quantity: item.quantity,
        userId: parseInt(user.userId), // Ensure userId is an integer
        requestedBy: user.fullName || user.userName || user.email || 'Anonymous User'
      }));
      console.log('Sending productRequests to distributors:', productRequests);
      const responses = await requestQuotation(productRequests);
      console.log('Distributor responses:', responses);
      setIsLoading(false);
      
      // Clear cart after successful quotation request
      setCartItems([]);
      localStorage.removeItem('cartItems');
      
      // Show success message
      setSuccessMessage('Quotation request submitted successfully! We will respond within 24 hours.');
      setShowSuccess(true);
      
      // Auto-hide success message after 5 seconds
      setTimeout(() => {
        setShowSuccess(false);
        setSuccessMessage('');
      }, 5000);
      
    } catch (err) {
      setIsLoading(false);
      alert('Failed to submit quotation request: ' + (err.message || err));
    }
  };

  const handleContinueShopping = () => {
    navigate('/product-catalog');
  };

  // Calculate totals
  const subtotal = cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);
  const tax = subtotal * 0.08; // 8% tax
  const shipping = subtotal > 100 ? 0 : 15.99; // Free shipping over $100
  const total = subtotal + tax + shipping;
  const itemCount = cartItems.reduce((sum, item) => sum + item.quantity, 0);

  // Show empty cart if no items and no success message
  if (cartItems.length === 0 && !showSuccess) {
    return <EmptyCart />;
  }

  return (
    <div className="min-h-screen bg-background">
      <Header />
      
      <div className="pt-20 pb-12">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          {/* Success Message */}
          {showSuccess && (
            <div className="mb-6 p-4 bg-green-50 border border-green-200 rounded-lg">
              <div className="flex items-center">
                <div className="flex-shrink-0">
                  <svg className="h-5 w-5 text-green-400" viewBox="0 0 20 20" fill="currentColor">
                    <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clipRule="evenodd" />
                  </svg>
                </div>
                <div className="ml-3 flex-1">
                  <p className="text-sm font-medium text-green-800">
                    {successMessage}
                  </p>
                  <div className="mt-2 flex gap-2">
                    <button
                      onClick={() => navigate('/customer-dashboard')}
                      className="text-sm text-green-600 hover:text-green-800 underline"
                    >
                      View in Dashboard
                    </button>
                    <button
                      onClick={() => navigate('/cart-quotations')}
                      className="text-sm text-green-600 hover:text-green-800 underline"
                    >
                      View Quotations
                    </button>
                  </div>
                </div>
                <div className="ml-auto pl-3">
                  <button
                    onClick={() => setShowSuccess(false)}
                    className="inline-flex text-green-400 hover:text-green-600"
                  >
                    <span className="sr-only">Close</span>
                    <svg className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                      <path fillRule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clipRule="evenodd" />
                    </svg>
                  </button>
                </div>
              </div>
            </div>
          )}

          {/* Show cart content only if there are items */}
          {cartItems.length > 0 ? (
            <>
              {/* Header */}
              <div className="flex items-center justify-between mb-8">
                <div>
                  <h1 className="text-3xl font-bold text-foreground">
                    Shopping Cart
                  </h1>
                  <p className="text-muted-foreground mt-2">
                    {itemCount} {itemCount === 1 ? 'item' : 'items'} in your cart
                  </p>
                </div>
                <div className="flex gap-2">
                  <Button
                    variant="outline"
                    onClick={handleContinueShopping}
                    iconName="ArrowLeft"
                    iconPosition="left"
                    className="hidden md:flex"
                  >
                    Continue Shopping
                  </Button>
                  <Button
                    variant="destructive"
                    onClick={handleClearCart}
                    iconName="Trash2"
                    iconPosition="left"
                  >
                    Clear Cart
                  </Button>
                </div>
              </div>

              <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                {/* Cart Items */}
                <div className="lg:col-span-2">
                  <div className="space-y-4">
                    {cartItems.map((item, idx) => (
                      <CartItem
                        key={item.productId || item.id || idx}
                        item={item}
                        onUpdateQuantity={handleUpdateQuantity}
                        onRemove={handleRemoveItem}
                        onSaveForLater={handleSaveForLater}
                      />
                    ))}
                  </div>
                  
                  {/* Mobile Continue Shopping */}
                  <div className="mt-6 md:hidden">
                    <Button
                      variant="outline"
                      fullWidth
                      onClick={handleContinueShopping}
                      iconName="ArrowLeft"
                      iconPosition="left"
                    >
                      Continue Shopping
                    </Button>
                  </div>
                </div>

                {/* Cart Summary */}
                <div className="lg:col-span-1">
                  <CartSummary
                    itemCount={itemCount}
                    cartItems={cartItems}
                    onRequestQuotation={handleRequestQuotation}
                    isLoading={isLoading}
                  />
                </div>
              </div>

              {/* Saved for Later */}
              <SavedForLater
                items={savedItems}
                onMoveToCart={handleMoveToCart}
                onRemove={handleRemoveSaved}
              />

              {/* Recently Viewed */}
              <RecentlyViewed
                items={recentlyViewed}
                onAddToCart={handleAddToCart}
              />
            </>
          ) : (
            /* Show empty cart with success message if no items but success is shown */
            <div className="text-center py-12">
              <h2 className="text-2xl font-bold text-foreground mb-4">
                Quotation Request Submitted!
              </h2>
              <p className="text-muted-foreground mb-8">
                Your quotation request has been successfully submitted. We'll get back to you within 24 hours.
              </p>
              <div className="flex flex-col sm:flex-row gap-4 justify-center">
                <Button
                  variant="default"
                  onClick={() => navigate('/product-catalog')}
                  iconName="Package"
                  iconPosition="left"
                >
                  Continue Shopping
                </Button>
                <Button
                  variant="outline"
                  onClick={() => navigate('/customer-dashboard')}
                  iconName="LayoutDashboard"
                  iconPosition="left"
                >
                  Go to Dashboard
                </Button>
                <Button
                  variant="outline"
                  onClick={() => navigate('/cart-quotations')}
                  iconName="FileText"
                  iconPosition="left"
                >
                  Final Quotations
                </Button>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default ShoppingCart;