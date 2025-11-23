import React, { useEffect, useState } from 'react';
// import Header from '../../components/ui/Header';

const CartQuotationsPage = () => {
  const [quotations, setQuotations] = useState([]);
  const [productDetails, setProductDetails] = useState({});
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchQuotations = async () => {
    try {
      const res = await fetch('http://localhost:5254/api/FullyComparedResponse');
      if (!res.ok) throw new Error('Failed to fetch quotations');
      const data = await res.json();
      console.log('Quotations:', data);
      return data;
    } catch (err) {
      setError(err.message);
      return [];
    }
  };

  const fetchProductDetails = async (quotationsData) => {
    const details = {};
    await Promise.all(
      quotationsData.map(async (q) => {
        console.log('Fetching product details for ProductId:', q.productId);
        try {
          const res = await fetch(`http://localhost:5254/api/Product/${q.productId}`);
          if (res.ok) {
            const prod = await res.json();
            details[q.productId] = prod;
            console.log('Product details for', q.productId, ':', prod);
          } else {
            console.warn('Failed to fetch product details for:', q.productId);
          }
        } catch (err) {
          console.warn('Error fetching product details for:', q.productId, err);
        }
      })
    );
    setProductDetails(details);
  };

  useEffect(() => {
    const loadData = async () => {
      try {
        setLoading(true);
        setError(null);

        // Fetch quotations first
        const quotationsData = await fetchQuotations();
        setQuotations(quotationsData);

        // Then fetch product details for each quotation
        await fetchProductDetails(quotationsData);

        setLoading(false);
      } catch (err) {
        console.error('Error loading data:', err);
        setError(err.message);
        setLoading(false);
      }
    };
    loadData();
  }, []);

  const handleConfirmQuotation = async (quotation) => {
    try {
      const user = JSON.parse(localStorage.getItem('user'));
      const userId = user?.userId || 1;
      
      const order = {
        UserId: Number(userId),
        OrderDate: new Date().toISOString(),
        Status: 'Confirmed', // This will trigger distributor notification
        ProductId: quotation.productId,
        Price: quotation.price,
        Stock: quotation.stock,
        QuotationRequestId: quotation.quotationRequestId,
        QuotationResponseId: quotation.quotationResponseId
      };

      // Create order
      const orderRes = await fetch('http://localhost:5254/api/Orders', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(order)
      });

      if (!orderRes.ok) {
        const errorText = await orderRes.text();
        throw new Error(`Failed to create order: ${errorText}`);
      }

      // Remove quotation from FullyComparedResponses
      const deleteRes = await fetch(`http://localhost:5254/api/Quotation/remove-quotation/${quotation.id}`, {
        method: 'DELETE'
      });

      if (!deleteRes.ok) {
        console.warn('Failed to remove quotation from database, but order was created');
      }

      // Remove from frontend state
      setQuotations(prev => prev.filter(q => q.id !== quotation.id));
      
      // Clear cart items from localStorage since order is confirmed
      localStorage.removeItem('cartItems');
      
      alert('Order confirmed and saved! Quotation has been removed and cart cleared. Order sent to distributors.');
    } catch (err) {
      alert('Error confirming order: ' + err.message);
      console.error('Order confirmation error:', err);
    }
  };

  const handleDeclineQuotation = async (quotation) => {
    try {
      // Remove quotation from FullyComparedResponses
      const deleteRes = await fetch(`http://localhost:5254/api/Quotation/remove-quotation/${quotation.id}`, {
        method: 'DELETE'
      });

      if (!deleteRes.ok) {
        const errorText = await deleteRes.text();
        throw new Error(`Failed to remove quotation: ${errorText}`);
      }

      // Remove from frontend state
      setQuotations(prev => prev.filter(q => q.id !== quotation.id));
      
      alert('Quotation declined and removed.');
    } catch (err) {
      alert('Error declining quotation: ' + err.message);
      console.error('Quotation decline error:', err);
    }
  };

  if (loading) return (
    <div className="min-h-screen bg-background">
      <div className="p-8 text-center">
        <div className="flex items-center justify-center min-h-[200px]">
          <div className="flex items-center space-x-3">
            <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-primary"></div>
            <span className="text-muted-foreground">Loading quotations...</span>
          </div>
        </div>
      </div>
    </div>
  );
  
  if (error) return (
    <div className="min-h-screen bg-background">
      <div className="p-8 text-center text-red-600">Error: {error}</div>
    </div>
  );

  return (
    <div className="min-h-screen bg-background">
      <div className="max-w-3xl mx-auto p-8">
        <h2 className="text-2xl font-bold mb-6 text-primary">Final Quotations</h2>
        {quotations.length === 0 ? (
          <div className="text-muted-foreground">No quotations found.</div>
        ) : (
          <div className="grid gap-6">
            {quotations.map((q) => {
              const prod = productDetails[q.productId] || {};
              const photoUrl = prod.url || prod.Url || null;
              console.log('Card:', {
                productId: q.productId,
                prod,
                photoUrl
              });
              return (
                <div key={q.id} className="bg-card border border-border rounded-lg shadow p-6 flex flex-col md:flex-row md:items-center mb-6">
                  <div className="flex-1">
                    <div className="text-xs text-muted-foreground mb-2">
                      Quotation ID: <span className="font-semibold text-foreground">{q.id}</span>
                    </div>
                    <div className="text-lg font-bold text-primary mb-1">{prod.name || prod.Name || 'Product Name Not Available'}</div>
                    <div className="text-md text-foreground mb-2">{prod.description || prod.Description || 'Product description not available'}</div>
                    {photoUrl && (
                      <img src={photoUrl} alt={prod.name || prod.Name} className="w-32 h-32 object-cover rounded mb-2" />
                    )}
                    <div className="text-md text-foreground mb-2">
                      Price: <span className="font-semibold">${q.price || '-'}</span>
                    </div>
                    <div className="text-sm text-muted-foreground">
                      Stock: {q.stock || '-'} available
                    </div>
                  </div>
                  <div className="flex flex-col md:flex-row gap-2 md:ml-auto md:items-center">
                    <button
                      className="bg-primary text-primary-foreground px-4 py-2 rounded-lg font-semibold shadow hover:bg-primary/90 transition w-full md:w-auto border border-primary"
                      onClick={() => handleConfirmQuotation(q)}
                    >
                      Confirm
                    </button>
                    <button 
                      className="bg-muted text-foreground px-4 py-2 rounded-lg font-semibold shadow hover:bg-muted/80 transition w-full md:w-auto border border-border"
                      onClick={() => handleDeclineQuotation(q)}
                    >
                      Decline
                    </button>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>
    </div>
  );
};

export default CartQuotationsPage;
