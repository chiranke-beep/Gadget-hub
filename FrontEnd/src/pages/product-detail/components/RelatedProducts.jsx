import React, { useRef } from 'react';
import { Link } from 'react-router-dom';
import Image from '../../../components/AppImage';
import Icon from '../../../components/AppIcon';
import Button from '../../../components/ui/Button';

const RelatedProducts = ({ products, onAddToQuote }) => {
  const scrollContainerRef = useRef(null);

  const scroll = (direction) => {
    const container = scrollContainerRef.current;
    if (container) {
      const scrollAmount = 300;
      container.scrollBy({
        left: direction === 'left' ? -scrollAmount : scrollAmount,
        behavior: 'smooth'
      });
    }
  };

  const handleQuickQuote = (product, e) => {
    e.preventDefault();
    e.stopPropagation();
    onAddToQuote(product, 1);
  };

  return (
    <div className="bg-card rounded-lg p-6">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-xl font-bold text-foreground flex items-center">
          <Icon name="Package" size={20} className="mr-2" />
          Related Products
        </h2>
        <div className="flex space-x-2">
          <button
            onClick={() => scroll('left')}
            className="w-10 h-10 rounded-full border border-border flex items-center justify-center hover:bg-muted transition-colors duration-150"
          >
            <Icon name="ChevronLeft" size={16} />
          </button>
          <button
            onClick={() => scroll('right')}
            className="w-10 h-10 rounded-full border border-border flex items-center justify-center hover:bg-muted transition-colors duration-150"
          >
            <Icon name="ChevronRight" size={16} />
          </button>
        </div>
      </div>

      <div
        ref={scrollContainerRef}
        className="flex space-x-4 overflow-x-auto scrollbar-hide pb-2"
        style={{ scrollbarWidth: 'none', msOverflowStyle: 'none' }}
      >
        {products.map((product) => (
          <Link
            key={product.id}
            to={`/product-detail?id=${product.id}`}
            className="flex-shrink-0 w-64 bg-background rounded-lg border border-border hover:border-primary transition-all duration-200 hover:shadow-lg group"
          >
            <div className="relative overflow-hidden rounded-t-lg">
              <div className="aspect-square">
                <Image
                  src={product.image}
                  alt={product.name}
                  className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-300"
                />
              </div>
              {product.discount && (
                <div className="absolute top-2 left-2 bg-accent text-accent-foreground px-2 py-1 rounded text-xs font-medium">
                  -{product.discount}%
                </div>
              )}
              {product.isNew && (
                <div className="absolute top-2 right-2 bg-primary text-primary-foreground px-2 py-1 rounded text-xs font-medium">
                  New
                </div>
              )}
            </div>

            <div className="p-4">
              <h3 className="font-semibold text-foreground mb-2 line-clamp-2 group-hover:text-primary transition-colors duration-150">
                {product.name}
              </h3>
              
              <div className="flex items-center space-x-1 mb-2">
                {[...Array(5)].map((_, i) => (
                  <Icon
                    key={i}
                    name="Star"
                    size={12}
                    className={`${
                      i < Math.floor(product.rating)
                        ? 'text-warning fill-current' :'text-muted'
                    }`}
                  />
                ))}
                <span className="text-xs text-muted-foreground ml-1">
                  ({product.reviewCount})
                </span>
              </div>

              <div className="flex items-center justify-between mb-3">
                <div className="flex items-baseline space-x-2">
                  <span className="text-lg font-bold text-primary">
                    ${product.price.toLocaleString()}
                  </span>
                  {product.originalPrice && product.originalPrice > product.price && (
                    <span className="text-sm text-muted-foreground line-through">
                      ${product.originalPrice.toLocaleString()}
                    </span>
                  )}
                </div>
              </div>

              <div className="flex space-x-2">
                <Button
                  variant="outline"
                  size="sm"
                  fullWidth
                  onClick={(e) => handleQuickQuote(product, e)}
                  iconName="MessageSquare"
                  iconPosition="left"
                >
                  Quick Quote
                </Button>
              </div>

              <div className="flex items-center justify-between mt-3 text-xs text-muted-foreground">
                <div className="flex items-center space-x-1">
                  <Icon name="Package" size={12} />
                  <span>{product.stock > 0 ? 'In Stock' : 'Out of Stock'}</span>
                </div>
                <div className="flex items-center space-x-1">
                  <Icon name="Truck" size={12} />
                  <span>Free Shipping</span>
                </div>
              </div>
            </div>
          </Link>
        ))}
      </div>

      <div className="text-center mt-6">
        <Link to="/product-catalog">
          <Button variant="outline" iconName="ArrowRight" iconPosition="right">
            View All Products
          </Button>
        </Link>
      </div>
    </div>
  );
};

export default RelatedProducts;