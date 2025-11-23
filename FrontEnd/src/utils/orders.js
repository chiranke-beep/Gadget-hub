const API_BASE_URL = 'http://localhost:5254/api';

// Fetch all orders for a specific user
export const fetchUserOrders = async (userId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Orders/user/${userId}`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error('Error fetching user orders:', error);
    throw error;
  }
};

// Fetch a specific order by ID
export const fetchOrder = async (orderId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Orders/${orderId}`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error('Error fetching order:', error);
    throw error;
  }
};

// Create a new order
export const createOrder = async (orderData) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Orders`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(orderData),
    });
    
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    
    return await response.json();
  } catch (error) {
    console.error('Error creating order:', error);
    throw error;
  }
};

// Update order status
export const updateOrderStatus = async (orderId, status) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Orders/${orderId}/status`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ status }),
    });
    
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    
    return await response.json();
  } catch (error) {
    console.error('Error updating order status:', error);
    throw error;
  }
};

// Transform order data for frontend display
export const transformOrderForDisplay = (order) => {
  return {
    id: order.orderId,
    date: order.orderDate,
    status: order.status,
    total: order.price,
    productId: order.productId,
    stock: order.stock,
    quotationRequestId: order.quotationRequestId,
    quotationResponseId: order.quotationResponseId,
    items: [{
      id: order.productId,
      name: order.productName,
      quantity: 1,
      price: order.price,
      image: order.productImageUrl,
      description: order.productDescription
    }]
  };
}; 