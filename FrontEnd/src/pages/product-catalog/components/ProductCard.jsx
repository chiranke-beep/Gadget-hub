import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import AppModal from '../../../components/AppModal';
import { Link } from 'react-router-dom';
import Image from '../../../components/AppImage';
import Icon from '../../../components/AppIcon';
import Button from '../../../components/ui/Button';

const ProductCard = ({ product, onAddToCart }) => {
  const [quantity, setQuantity] = useState(1);
  const handleIncrease = () => {
    if (quantity < (product.stock || 99)) setQuantity(q => q + 1);
  };
  const handleDecrease = () => {
    if (quantity > 1) setQuantity(q => q - 1);
  };

  // Support both backend and mock product shapes
  // Removed isHovered state, not needed
  const [isAddingToCart, setIsAddingToCart] = useState(false);
  const imageUrl = product.url || (product.images && product.images[0]) || product.image || '/assets/images/no_image.png';
  const id = product.productId || product.id;
  const navigate = useNavigate();

  const handleAddToCart = async (e) => {
    e.preventDefault();
    e.stopPropagation();
    setIsAddingToCart(true);
    try {
      await onAddToCart(product);
      setTimeout(() => setIsAddingToCart(false), 1000);
    } catch (error) {
      setIsAddingToCart(false);
    }
  };

  const formatPrice = (price) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(price);
  };

  return (
    <div
      className="block bg-card border border-border rounded-lg overflow-hidden transition-all duration-300"
    >
      {/* Product Image */}
      <div className="relative aspect-square overflow-hidden bg-muted">
        <Image
          src={imageUrl}
          alt={product.name}
          className="w-full h-full object-cover transition-transform duration-300"
          loading="lazy"
        />
        {/* Discount Badge */}
        {product.discount && (
          <div className="absolute top-2 left-2 bg-error text-error-foreground px-2 py-1 rounded-md text-xs font-medium">
            -{product.discount}%
          </div>
        )}

        {/* Stock Status */}
        {product.stock === 0 && (
          <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center">
            <span className="text-white font-medium">Out of Stock</span>
          </div>
        )}

        {/* Image Indicators */}
        {Array.isArray(product.images) && product.images.length > 1 && (
          <div className="absolute bottom-2 left-1/2 transform -translate-x-1/2 flex space-x-1">
            {product.images.map((_, index) => (
              <div
                key={product.id ? `${product.id}-img-${index}` : `img-${index}`}
                className={`w-1.5 h-1.5 rounded-full transition-colors duration-200 ${
                  index === currentImageIndex ? 'bg-white' : 'bg-white bg-opacity-50'
                }`}
              />
            ))}
          </div>
        )}
      </div>

      {/* Product Info */}
      <div className="p-4">
        {/* Brand */}
        <div className="text-xs text-muted-foreground mb-1 uppercase tracking-wide">
          {product.brand}
        </div>

        {/* Product Name */}

        <Link
          to={`/product-detail?id=${id}`}
          className="block"
          tabIndex={-1}
        >
          <h3 className="font-medium text-foreground mb-1 line-clamp-2 transition-colors duration-200">
            {product.name}
          </h3>
        </Link>
        {/* Product Description */}
        {product.description && (
          <div className="text-xs text-muted-foreground mb-2 line-clamp-2">
            {product.description}
          </div>
        )}

        {/* Key Specifications */}
        <div className="space-y-1 mb-3">
          {(Array.isArray(product.keySpecs) ? product.keySpecs.slice(0, 2) : []).map((spec, index) => (
            <div key={spec ? `${spec}-${index}` : `spec-${index}`} className="text-xs text-muted-foreground flex items-center space-x-1">
              <Icon name="Zap" size={12} />
              <span>{spec}</span>
            </div>
          ))}
        </div>

        {/* Rating */}

        {/* Rating (always filled stars, no review count) */}
        <div className="flex items-center space-x-1 mb-3">
          <div className="flex items-center">
            {[...Array(5)].map((_, i) => (
              <Icon
                key={i}
                name="Star"
                size={12}
                className="text-warning fill-current"
              />
            ))}
          </div>
        </div>

        {/* Add to Cart and More Details Buttons */}
        <div className="flex flex-col gap-2">
        {/* Quantity Controls */}
        <div className="flex items-center justify-center gap-2 mb-2">
          <button
            onClick={handleDecrease}
            className="w-8 h-8 p-0 border border-gray-300 rounded-lg text-gray-400 hover:text-gray-600 hover:border-gray-400 transition-colors duration-200 flex items-center justify-center"
            type="button"
          >
            <Icon name="Minus" className="w-4 h-4" />
          </button>
          <span className="text-base font-semibold w-8 text-center select-none">{quantity}</span>
          <button
            onClick={handleIncrease}
            className="w-8 h-8 p-0 border border-gray-300 rounded-lg text-gray-400 hover:text-gray-600 hover:border-gray-400 transition-colors duration-200 flex items-center justify-center"
            type="button"
          >
            <Icon name="Plus" className="w-4 h-4" />
          </button>
        </div>
          <Button
            variant="default"
            size="sm"
            fullWidth
            loading={isAddingToCart}
            disabled={product.stock === 0}
            onClick={e => { e.preventDefault(); e.stopPropagation(); setIsAddingToCart(true); onAddToCart({ ...product, quantity }); setTimeout(() => setIsAddingToCart(false), 1000); }}
            iconName={isAddingToCart ? "Check" : "ShoppingCart"}
            iconPosition="left"
          >
            {isAddingToCart ? 'Added!' : product.stock === 0 ? 'Out of Stock' : 'Add to Cart'}
          </Button>
        <Button
          variant="secondary"
          size="sm"
          fullWidth
          iconName="Info"
          iconPosition="left"
          className="w-full"
          type="button"
          onClick={() => navigate(`/product-detail?id=${id}`)}
        >
          More Details
        </Button>
      </div>

      {/* Modal for More Details removed. Navigation is now used. */}
      </div>
    </div>
  );
};

export default ProductCard;