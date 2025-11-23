import React, { useState } from 'react';
import Icon from '../../../components/AppIcon';
import Button from '../../../components/ui/Button';

const ProductInfo = ({ product, onAddToQuote }) => {
  const [quantity, setQuantity] = useState(1);
  const [isAddingToQuote, setIsAddingToQuote] = useState(false);

  const handleQuantityChange = (change) => {
    const newQuantity = quantity + change;
    if (newQuantity >= 1 && newQuantity <= 99) {
      setQuantity(newQuantity);
    }
  };

  const handleAddToQuote = async () => {
    setIsAddingToQuote(true);
    await onAddToQuote(product, quantity);
    setTimeout(() => setIsAddingToQuote(false), 1000);
  };

  const handleShare = () => {
    if (navigator.share) {
      navigator.share({
        title: product.name,
        text: product.description,
        url: window.location.href,
      });
    } else {
      navigator.clipboard.writeText(window.location.href);
    }
  };

  if (!product) {
    return null;
  }
  return (
    <div className="space-y-6">
      {/* Product Title and Rating */}
      <div>
        <h1 className="text-2xl md:text-3xl font-bold text-foreground mb-2">
          {product.name || 'Product'}
        </h1>
        <div className="flex items-center space-x-4 mb-4">
          <div className="flex items-center space-x-1">
            {[...Array(5)].map((_, i) => (
              <Icon
                key={i}
                name="Star"
                size={16}
                className={`${
                  i < Math.floor(product.rating || 0)
                    ? 'text-warning fill-current' :'text-muted'
                }`}
              />
            ))}
            <span className="text-sm text-muted-foreground ml-2">
              ({product.rating || 0})  {product.reviewCount || 0} reviews
            </span>
          </div>
        </div>
      </div>

      {/* Price (removed) */}

      {/* Description */}
      <div>
        <h3 className="text-lg font-semibold text-foreground mb-3">Description</h3>
        <div className="text-muted-foreground">
          {product.description ? product.description : 'No description available.'}
        </div>
      </div>

      {/* Key Features */}
      {Array.isArray(product.keyFeatures) && product.keyFeatures.length > 0 ? (
        <div>
          <h3 className="text-lg font-semibold text-foreground mb-3">Key Features</h3>
          <ul className="space-y-2">
            {product.keyFeatures.map((feature, index) => (
              <li key={index} className="flex items-start space-x-2">
                <Icon name="Check" size={16} className="text-accent mt-0.5 flex-shrink-0" />
                <span className="text-muted-foreground">{feature}</span>
              </li>
            ))}
          </ul>
        </div>
      ) : (
        <div>
          <h3 className="text-lg font-semibold text-foreground mb-3">Key Features</h3>
          <div className="text-muted-foreground">No key features listed.</div>
        </div>
      )}

      {/* Quantity and Quote Request */}
      <div className="bg-card p-4 rounded-lg space-y-4">
        <div>
          <label className="block text-sm font-medium text-foreground mb-2">
            Quantity
          </label>
          <div className="flex items-center space-x-3">
            <button
              onClick={() => handleQuantityChange(-1)}
              disabled={quantity <= 1}
              className="w-10 h-10 rounded-lg border border-border flex items-center justify-center hover:bg-muted disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-150"
            >
              <Icon name="Minus" size={16} />
            </button>
            <span className="text-lg font-medium text-foreground min-w-[3rem] text-center">
              {quantity}
            </span>
            <button
              onClick={() => handleQuantityChange(1)}
              disabled={quantity >= 99}
              className="w-10 h-10 rounded-lg border border-border flex items-center justify-center hover:bg-muted disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-150"
            >
              <Icon name="Plus" size={16} />
            </button>
          </div>
        </div>

        <Button
          variant="default"
          size="lg"
          fullWidth
          loading={isAddingToQuote}
          disabled={product.stock === 0}
          onClick={handleAddToQuote}
          iconName="MessageSquare"
          iconPosition="left"
        >
          {isAddingToQuote ? 'Adding to Quote...' : 'Request Quote'}
        </Button>

        <div className="flex space-x-2">
          <Button
            variant="outline"
            size="default"
            onClick={handleShare}
            iconName="Share2"
          >
            Share
          </Button>
        </div>
      </div>

      {/* Product Highlights */}
      <div className="grid grid-cols-2 gap-4">
        <div className="flex items-center space-x-2 text-sm">
          <Icon name="Truck" size={16} className="text-primary" />
          <span className="text-muted-foreground">Free Shipping</span>
        </div>
        <div className="flex items-center space-x-2 text-sm">
          <Icon name="Shield" size={16} className="text-primary" />
          <span className="text-muted-foreground">1 Year Warranty</span>
        </div>
        <div className="flex items-center space-x-2 text-sm">
          <Icon name="RotateCcw" size={16} className="text-primary" />
          <span className="text-muted-foreground">30-Day Returns</span>
        </div>
        <div className="flex items-center space-x-2 text-sm">
          <Icon name="Headphones" size={16} className="text-primary" />
          <span className="text-muted-foreground">24/7 Support</span>
        </div>
      </div>
    </div>
  );
};

export default ProductInfo;