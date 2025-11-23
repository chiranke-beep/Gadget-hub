import React from 'react';
import { Link } from 'react-router-dom';
import Button from '../../../components/ui/Button';
import Icon from '../../../components/AppIcon';


const EmptyCart = () => {
  const suggestedCategories = [
    { name: 'Graphics Cards', icon: 'Monitor', path: '/product-catalog?category=graphics-cards' },
    { name: 'Keyboards', icon: 'Keyboard', path: '/product-catalog?category=keyboards' },
    { name: 'Headsets', icon: 'Headphones', path: '/product-catalog?category=headsets' },
    { name: 'Power Banks', icon: 'Battery', path: '/product-catalog?category=power-banks' }
  ];

  return (
    <div className="min-h-screen bg-background pt-20">
      <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="text-center">
          <div className="w-64 h-64 mx-auto mb-8 relative">
            <div className="absolute inset-0 bg-muted rounded-full flex items-center justify-center">
              <Icon name="ShoppingCart" size={80} className="text-muted-foreground" />
            </div>
            <div className="absolute -top-4 -right-4 w-16 h-16 bg-accent/20 rounded-full flex items-center justify-center animate-bounce">
              <Icon name="Plus" size={24} className="text-accent" />
            </div>
          </div>
          
          <h1 className="text-3xl font-bold text-foreground mb-4">
            Your Cart is Empty
          </h1>
          
          <p className="text-lg text-muted-foreground mb-8 max-w-md mx-auto">
            Looks like you haven't added any items to your cart yet. Start exploring our amazing collection of PC components and gadgets!
          </p>
          
          <div className="flex flex-col sm:flex-row gap-4 justify-center mb-12">
            <Button
              variant="default"
              size="lg"
              iconName="Package"
              iconPosition="left"
              asChild
            >
              <Link to="/product-catalog">
                Browse Products
              </Link>
            </Button>
            
            <Button
              variant="outline"
              size="lg"
              iconName="Home"
              iconPosition="left"
              asChild
            >
              <Link to="/home-page">
                Back to Home
              </Link>
            </Button>

            <Button
              variant="outline"
              size="lg"
              iconName="FileText"
              iconPosition="left"
              asChild
            >
              <Link to="/cart-quotations">
                Final Quotations
              </Link>
            </Button>
          </div>
          
          <div className="max-w-2xl mx-auto">
            <h2 className="text-xl font-semibold text-foreground mb-6">
              Popular Categories
            </h2>
            
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              {suggestedCategories.map((category, index) => (
                <Link
                  key={index}
                  to={category.path}
                  className="group p-6 bg-card border border-border rounded-lg hover:border-primary transition-all duration-200 hover:transform hover:-translate-y-1"
                >
                  <div className="flex flex-col items-center text-center">
                    <div className="w-12 h-12 bg-primary/10 rounded-lg flex items-center justify-center mb-3 group-hover:bg-primary/20 transition-colors duration-200">
                      <Icon 
                        name={category.icon} 
                        size={24} 
                        className="text-primary" 
                      />
                    </div>
                    <h3 className="font-medium text-foreground group-hover:text-primary transition-colors duration-200">
                      {category.name}
                    </h3>
                  </div>
                </Link>
              ))}
            </div>
          </div>
          
          <div className="mt-12 p-6 bg-card border border-border rounded-lg max-w-md mx-auto">
            <div className="flex items-center space-x-3 mb-4">
              <Icon name="Gift" size={24} className="text-accent" />
              <h3 className="text-lg font-semibold text-foreground">
                Special Offers
              </h3>
            </div>
            
            <p className="text-sm text-muted-foreground mb-4">
              Get exclusive deals and discounts when you request quotations for bulk orders!
            </p>
            
            <div className="space-y-2 text-xs text-muted-foreground">
              <div className="flex items-center space-x-2">
                <Icon name="Check" size={14} className="text-success" />
                <span>Free shipping on orders over $100</span>
              </div>
              <div className="flex items-center space-x-2">
                <Icon name="Check" size={14} className="text-success" />
                <span>Bulk discount pricing available</span>
              </div>
              <div className="flex items-center space-x-2">
                <Icon name="Check" size={14} className="text-success" />
                <span>24-hour quotation response time</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EmptyCart;