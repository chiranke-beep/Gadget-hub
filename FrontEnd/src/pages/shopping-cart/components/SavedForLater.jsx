import React from 'react';
import { Link } from 'react-router-dom';
import Image from '../../../components/AppImage';
import Icon from '../../../components/AppIcon';
import Button from '../../../components/ui/Button';

const SavedForLater = ({ items, onMoveToCart, onRemove }) => {
  if (!items || items.length === 0) {
    return null;
  }

  return (
    <div className="mt-12">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-xl font-semibold text-foreground">
          Saved for Later ({items.length})
        </h2>
        <Button
          variant="ghost"
          size="sm"
          iconName="Heart"
          iconPosition="left"
          className="text-muted-foreground"
        >
          Manage Saved Items
        </Button>
      </div>
      
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
        {items.map((item) => (
          <div
            key={item.id}
            className="bg-card border border-border rounded-lg p-4 group hover:border-primary/50 transition-all duration-200"
          >
            <div className="relative mb-4">
              <Link to={`/product-detail?id=${item.id}`}>
                <div className="aspect-square rounded-lg overflow-hidden bg-muted">
                  <Image
                    src={item.image}
                    alt={item.name}
                    className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-200"
                  />
                </div>
              </Link>
              
              <button
                onClick={() => onRemove(item.id)}
                className="absolute top-2 right-2 w-8 h-8 bg-background/80 backdrop-blur-sm rounded-full flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity duration-200 hover:bg-error hover:text-error-foreground"
              >
                <Icon name="X" size={16} />
              </button>
            </div>
            
            <div className="space-y-2">
              <Link 
                to={`/product-detail?id=${item.id}`}
                className="block hover:text-primary transition-colors duration-150"
              >
                <h3 className="font-medium text-foreground text-sm line-clamp-2">
                  {item.name}
                </h3>
              </Link>
              
              <p className="text-xs text-muted-foreground">
                {item.category}
              </p>
              
              <div className="flex items-center justify-between">
                <p className="text-lg font-bold text-foreground">
                  ${item.price.toFixed(2)}
                </p>
                
                {item.originalPrice && item.originalPrice > item.price && (
                  <p className="text-sm text-muted-foreground line-through">
                    ${item.originalPrice.toFixed(2)}
                  </p>
                )}
              </div>
              
              <div className="flex items-center space-x-2 pt-2">
                <Button
                  variant="outline"
                  size="sm"
                  onClick={() => onMoveToCart(item.id)}
                  iconName="ShoppingCart"
                  iconPosition="left"
                  className="flex-1"
                >
                  Add to Cart
                </Button>
                
                <Button
                  variant="ghost"
                  size="sm"
                  onClick={() => onRemove(item.id)}
                  iconName="Trash2"
                  className="text-error hover:text-error hover:bg-error/10"
                />
              </div>
              
              {item.stock <= 5 && (
                <div className="flex items-center space-x-1 text-xs text-warning">
                  <Icon name="AlertTriangle" size={12} />
                  <span>Only {item.stock} left</span>
                </div>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default SavedForLater;