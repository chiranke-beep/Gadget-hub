import React from 'react';
import { Link } from 'react-router-dom';
import Image from '../../../components/AppImage';
import Icon from '../../../components/AppIcon';
import Button from '../../../components/ui/Button';

const RecentlyViewed = ({ items, onAddToCart }) => {
  if (!items || items.length === 0) {
    return null;
  }

  return (
    <div className="mt-12">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-xl font-semibold text-foreground">
          Recently Viewed
        </h2>
        <Button
          variant="ghost"
          size="sm"
          iconName="Eye"
          iconPosition="left"
          className="text-muted-foreground"
        >
          View All
        </Button>
      </div>
      
      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6 gap-4">
        {items.slice(0, 6).map((item) => (
          <div
            key={item.id}
            className="bg-card border border-border rounded-lg p-3 group hover:border-primary/50 transition-all duration-200 hover:transform hover:-translate-y-1"
          >
            <div className="relative mb-3">
              <Link to={`/product-detail?id=${item.id}`}>
                <div className="aspect-square rounded-lg overflow-hidden bg-muted">
                  <Image
                    src={item.image}
                    alt={item.name}
                    className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-200"
                  />
                </div>
              </Link>
              
              {item.isNew && (
                <div className="absolute top-2 left-2 bg-accent text-accent-foreground text-xs font-medium px-2 py-1 rounded-full">
                  New
                </div>
              )}
              
              {item.discount && (
                <div className="absolute top-2 right-2 bg-error text-error-foreground text-xs font-medium px-2 py-1 rounded-full">
                  -{item.discount}%
                </div>
              )}
            </div>
            
            <div className="space-y-2">
              <Link 
                to={`/product-detail?id=${item.id}`}
                className="block hover:text-primary transition-colors duration-150"
              >
                <h3 className="font-medium text-foreground text-xs line-clamp-2">
                  {item.name}
                </h3>
              </Link>
              
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm font-bold text-foreground">
                    ${item.price.toFixed(2)}
                  </p>
                  {item.originalPrice && item.originalPrice > item.price && (
                    <p className="text-xs text-muted-foreground line-through">
                      ${item.originalPrice.toFixed(2)}
                    </p>
                  )}
                </div>
                
                <div className="flex items-center space-x-1 text-xs text-muted-foreground">
                  <Icon name="Star" size={12} className="fill-current text-warning" />
                  <span>{item.rating}</span>
                </div>
              </div>
              
              <Button
                variant="outline"
                size="xs"
                onClick={() => onAddToCart(item)}
                iconName="Plus"
                iconPosition="left"
                className="w-full"
              >
                Add
              </Button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default RecentlyViewed;