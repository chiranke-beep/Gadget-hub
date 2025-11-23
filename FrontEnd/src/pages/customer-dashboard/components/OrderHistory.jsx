import React, { useState, useEffect } from 'react';
import Button from '../../../components/ui/Button';
import Input from '../../../components/ui/Input';
import Icon from '../../../components/AppIcon';
import Image from '../../../components/AppImage';
import { fetchUserOrders, transformOrderForDisplay } from '../../../utils/orders';

const OrderHistory = () => {
  const [orders, setOrders] = useState([]);
  const [filteredOrders, setFilteredOrders] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [statusFilter, setStatusFilter] = useState('all');
  const [isLoading, setIsLoading] = useState(true);

  // Fetch orders from backend
  useEffect(() => {
    const fetchOrders = async () => {
      setIsLoading(true);
      try {
        const user = JSON.parse(localStorage.getItem('user'));
        let userId = user?.userId;
        console.log('User from localStorage:', user);
        console.log('UserId:', userId);
        
        // Fallback to user ID 1 for testing if no user found
        if (!userId) {
          console.log('No user found in localStorage, using fallback userId: 1');
          userId = 1;
        }
        
        if (!userId) throw new Error('User not logged in');
        
        // Use the new orders utility function
        console.log('Fetching orders for user:', userId);
        const userOrders = await fetchUserOrders(userId);
        console.log('Raw orders from API:', userOrders);
        
        // Transform the data to match the frontend structure
        const transformedOrders = userOrders.map(transformOrderForDisplay);
        console.log('Transformed orders:', transformedOrders);
        
        setOrders(transformedOrders);
        setFilteredOrders(transformedOrders);
      } catch (err) {
        console.error('Error fetching orders:', err);
        setOrders([]);
        setFilteredOrders([]);
      } finally {
        setIsLoading(false);
      }
    };
    fetchOrders();
  }, []);

  useEffect(() => {
    let filtered = orders;

    // Filter by search query
    if (searchQuery) {
      filtered = filtered.filter(order =>
        String(order.id).toLowerCase().includes(searchQuery.toLowerCase()) ||
        order.items.some(item =>
          item.name.toLowerCase().includes(searchQuery.toLowerCase())
        )
      );
    }

    // Filter by status
    if (statusFilter !== 'all') {
      filtered = filtered.filter(order => order.status === statusFilter);
    }

    setFilteredOrders(filtered);
  }, [orders, searchQuery, statusFilter]);

  const getStatusColor = (status) => {
    switch (status) {
      case 'delivered':
        return 'text-success bg-success/10 border-success/20';
      case 'shipped':
        return 'text-primary bg-primary/10 border-primary/20';
      case 'processing':
        return 'text-warning bg-warning/10 border-warning/20';
      case 'cancelled':
        return 'text-error bg-error/10 border-error/20';
      case 'confirmed':
        return 'text-success bg-success/10 border-success/20';
      default:
        return 'text-muted-foreground bg-muted border-border';
    }
  };

  const getStatusIcon = (status) => {
    switch (status) {
      case 'delivered':
        return 'CheckCircle';
      case 'shipped':
        return 'Truck';
      case 'processing':
        return 'Clock';
      case 'cancelled':
        return 'XCircle';
      case 'confirmed':
        return 'CheckCircle';
      default:
        return 'Package';
    }
  };

  console.log('Current orders state:', orders);
  console.log('Current filtered orders:', filteredOrders);
  console.log('Is loading:', isLoading);

  if (isLoading) {
    return (
      <div className="space-y-4">
        <div className="bg-card border border-border rounded-lg p-6">
          <div className="flex items-center justify-center min-h-[200px]">
            <div className="flex items-center space-x-3">
              <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-primary"></div>
              <span className="text-muted-foreground">Loading orders...</span>
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="bg-card border border-border rounded-lg p-6">
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div>
            <h2 className="text-xl font-semibold text-foreground">Order History</h2>
            <p className="text-muted-foreground">Track and manage your orders</p>
            <p className="text-xs text-muted-foreground">Debug: {orders.length} orders loaded</p>
          </div>
          <Button
            onClick={() => window.open('/product-catalog', '_blank')}
            iconName="ShoppingBag"
            iconPosition="left"
          >
            Shop Now
          </Button>
        </div>
      </div>

      {/* Filters */}
      <div className="bg-card border border-border rounded-lg p-6">
        <div className="flex flex-col sm:flex-row gap-4">
          <div className="flex-1">
            <Input
              type="search"
              placeholder="Search orders by ID or product name..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="w-full"
            />
          </div>
          <div className="flex space-x-2">
            {['all', 'processing', 'confirmed', 'shipped', 'delivered'].map((status) => (
              <button
                key={status}
                onClick={() => setStatusFilter(status)}
                className={`px-4 py-2 rounded-lg text-sm font-medium transition-all duration-150 ${
                  statusFilter === status
                    ? 'bg-primary text-primary-foreground'
                    : 'bg-muted text-muted-foreground hover:text-foreground hover:bg-muted/80'
                }`}
              >
                {status.charAt(0).toUpperCase() + status.slice(1)}
              </button>
            ))}
          </div>
        </div>
      </div>

      {/* Orders List */}
      {filteredOrders.length === 0 ? (
        <div className="bg-card border border-border rounded-lg p-12 text-center">
          <Icon name="Package" size={48} className="text-muted-foreground mx-auto mb-4" />
          <h3 className="text-lg font-semibold text-foreground mb-2">No orders found</h3>
          <p className="text-muted-foreground mb-6">
            {searchQuery || statusFilter !== 'all' ?'Try adjusting your search or filter criteria' : "You haven't placed any orders yet"
            }
          </p>
          <Button
            onClick={() => window.open('/product-catalog', '_blank')}
            iconName="ShoppingBag"
            iconPosition="left"
          >
            Start Shopping
          </Button>
        </div>
      ) : (
        <div className="space-y-4">
          {filteredOrders.map((order) => (
            <div key={order.id} className="bg-card border border-border rounded-lg p-6">
              {/* Order Header */}
              <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 mb-4">
                <div className="flex items-center space-x-4">
                  <div>
                    <h3 className="text-lg font-semibold text-foreground">
                      Order {order.id}
                    </h3>
                    <p className="text-sm text-muted-foreground">
                      Placed on {new Date(order.date).toLocaleDateString()}
                    </p>
                    {order.quotationRequestId && (
                      <p className="text-xs text-muted-foreground">
                        Quotation Request: #{order.quotationRequestId}
                      </p>
                    )}
                  </div>
                  <div className={`px-3 py-1 rounded-full text-sm font-medium border ${getStatusColor(order.status)}`}>
                    <div className="flex items-center space-x-1">
                      <Icon name={getStatusIcon(order.status)} size={14} />
                      <span>{order.status.charAt(0).toUpperCase() + order.status.slice(1)}</span>
                    </div>
                  </div>
                </div>
                <div className="text-right">
                  <p className="text-xl font-bold text-foreground">
                    ${order.total.toFixed(2)}
                  </p>
                  <p className="text-sm text-muted-foreground">
                    {order.items.length} {order.items.length === 1 ? 'item' : 'items'}
                  </p>
                  {order.stock > 0 && (
                    <p className="text-xs text-success">
                      Stock: {order.stock} available
                    </p>
                  )}
                </div>
              </div>

              {/* Order Items */}
              <div className="space-y-3 mb-4">
                {order.items.slice(0, 2).map((item) => (
                  <div key={item.id} className="flex items-center space-x-3">
                    <div className="w-12 h-12 rounded-lg overflow-hidden bg-muted flex-shrink-0">
                      <Image
                        src={item.image}
                        alt={item.name}
                        className="w-full h-full object-cover"
                      />
                    </div>
                    <div className="flex-1 min-w-0">
                      <p className="text-sm font-medium text-foreground truncate">
                        {item.name}
                      </p>
                      <p className="text-xs text-muted-foreground">
                        Qty: {item.quantity} × ${item.price.toFixed(2)}
                      </p>
                      {item.description && (
                        <p className="text-xs text-muted-foreground truncate">
                          {item.description}
                        </p>
                      )}
                    </div>
                  </div>
                ))}
                {order.items.length > 2 && (
                  <p className="text-sm text-muted-foreground pl-15">
                    +{order.items.length - 2} more {order.items.length - 2 === 1 ? 'item' : 'items'}
                  </p>
                )}
              </div>

              {/* Order Actions */}
              <div className="flex flex-col sm:flex-row gap-3 pt-4 border-t border-border">
                <Button
                  variant="outline"
                  size="sm"
                  iconName="Eye"
                  iconPosition="left"
                  className="flex-1 sm:flex-initial"
                >
                  View Details
                </Button>
                {order.status === 'shipped' && order.trackingNumber && (
                  <Button
                    variant="outline"
                    size="sm"
                    iconName="Truck"
                    iconPosition="left"
                    className="flex-1 sm:flex-initial"
                  >
                    Track Package
                  </Button>
                )}
                <Button
                  variant="outline"
                  size="sm"
                  iconName="RotateCcw"
                  iconPosition="left"
                  className="flex-1 sm:flex-initial"
                >
                  Reorder
                </Button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default OrderHistory;