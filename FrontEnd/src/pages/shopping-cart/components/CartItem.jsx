import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import Image from '../../../components/AppImage';
import Icon from '../../../components/AppIcon';
import Button from '../../../components/ui/Button';

const CartItem = ({ item, onUpdateQuantity, onRemove, onSaveForLater }) => {
  const [isRemoving, setIsRemoving] = useState(false);
  const [isUpdatingQty, setIsUpdatingQty] = useState(false);

  const uniqueId = item.productId || item.id;
  // Use the same image fallback logic as ProductCard
  const imageUrl = item.url || (item.images && item.images[0]) || item.image || '/assets/images/no_image.png';
  const handleRemove = async () => {
    setIsRemoving(true);
    setTimeout(() => {
      onRemove(uniqueId);
    }, 300);
  };

  const handleQuantityChange = async (newQuantity) => {
    if (newQuantity > 0 && item.stock && newQuantity <= item.stock) {
      setIsUpdatingQty(true);
      try {
        await onUpdateQuantity(uniqueId, newQuantity);
      } finally {
        setIsUpdatingQty(false);
      }
    }
  };

  return (
    <div className={`bg-card border border-border rounded-lg p-4 transition-all duration-300 ${
      isRemoving ? 'opacity-0 transform scale-95' : 'opacity-100 transform scale-100'
    }`}>
      {/* Mobile Layout */}
      <div className="md:hidden">
        <div className="flex space-x-4">
          <div className="flex-shrink-0">
            <Link to={`/product-detail?id=${uniqueId}`}>
              <div className="w-20 h-20 rounded-lg overflow-hidden bg-muted">
                <Image
                  src={imageUrl}
                  alt={item.name}
                  className="w-full h-full object-cover hover:scale-105 transition-transform duration-200"
                />
              </div>
            </Link>
          </div>
          <div className="flex-1 min-w-0">
            <Link 
              to={`/product-detail?id=${uniqueId}`}
              className="block hover:text-primary transition-colors duration-150"
            >
              <h3 className="font-semibold text-foreground text-sm line-clamp-2">
                {item.name}
              </h3>
            </Link>
            <p className="text-xs text-muted-foreground mt-1">
              {item.category}
            </p>
            {item.specifications && (
              <p className="text-xs text-muted-foreground mt-1">
                {item.specifications}
              </p>
            )}
            {/* Quantity Removed - Mobile */}
          </div>
        </div>
        <div className="flex items-center justify-between mt-4 pt-4 border-t border-border">
          <Button
            variant="ghost"
            size="sm"
            onClick={() => onSaveForLater(uniqueId)}
            iconName="Heart"
            iconPosition="left"
            className="text-muted-foreground hover:text-foreground"
          >
            Save for Later
          </Button>
          <Button
            variant="ghost"
            size="sm"
            onClick={handleRemove}
            iconName="Trash2"
            iconPosition="left"
            className="text-error hover:text-error hover:bg-error/10"
          >
            Remove
          </Button>
        </div>
      </div>
      {/* Desktop Layout */}
      <div className="hidden md:flex items-center space-x-6">
        <div className="flex-shrink-0">
          <Link to={`/product-detail?id=${uniqueId}`}>
            <div className="w-24 h-24 rounded-lg overflow-hidden bg-muted">
              <Image
                src={imageUrl}
                alt={item.name}
                className="w-full h-full object-cover hover:scale-105 transition-transform duration-200"
              />
            </div>
          </Link>
        </div>
        <div className="flex-1 min-w-0">
          <Link 
            to={`/product-detail?id=${uniqueId}`}
            className="block hover:text-primary transition-colors duration-150"
          >
            <h3 className="font-semibold text-foreground text-base line-clamp-1">
              {item.name}
            </h3>
          </Link>
          <p className="text-sm text-muted-foreground mt-1">
            {item.category}
          </p>
          {item.specifications && (
            <p className="text-sm text-muted-foreground mt-1">
              {item.specifications}
            </p>
          )}
          <div className="flex items-center space-x-4 mt-2">
            <Button
              variant="ghost"
              size="sm"
              onClick={() => onSaveForLater(uniqueId)}
              iconName="Heart"
              iconPosition="left"
              className="text-muted-foreground hover:text-foreground"
            >
              Save for Later
            </Button>
            <Button
              variant="ghost"
              size="sm"
              onClick={handleRemove}
              iconName="Trash2"
              iconPosition="left"
              className="text-error hover:text-error hover:bg-error/10"
            >
              Remove
            </Button>
          </div>
        </div>
        {/* Quantity Removed - Desktop */}
        <div className="text-right min-w-0 w-32">
          {/* Price display removed */}
        </div>
      </div>
      {item.stock <= 5 && (
        <div className="mt-3 p-2 bg-warning/10 border border-warning/20 rounded-lg">
          <div className="flex items-center space-x-2">
            <Icon name="AlertTriangle" size={16} className="text-warning" />
            <p className="text-sm text-warning">
              Only {item.stock} left in stock
            </p>
          </div>
        </div>
      )}
    </div>
  );
};

export default CartItem;