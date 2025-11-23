import React, { useState, useEffect, useCallback } from 'react';
import { fetchProducts } from '../../utils/api';
import { useLocation, useNavigate } from 'react-router-dom';
import Header from '../../components/ui/Header';
import CategoryTabs from './components/CategoryTabs';
// import SortDropdown from './components/SortDropdown';
import ProductGrid from './components/ProductGrid';
import LoadMoreButton from './components/LoadMoreButton';


const ProductCatalog = () => {
  // Categories state for dynamic tabs
  const [categories, setCategories] = useState([{ id: 'all', name: 'All Products', icon: 'Grid3X3' }]);
  const location = useLocation();
  const navigate = useNavigate();
  
  // State management
  const [activeCategory, setActiveCategory] = useState('all');
  const [sortBy, setSortBy] = useState('relevance');
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [loadingMore, setLoadingMore] = useState(false);
  const [hasMore, setHasMore] = useState(true);
  const [page, setPage] = useState(1);
  // Sync cartItems with localStorage for cross-page consistency
  const [cartItems, setCartItems] = useState(() => {
    const stored = localStorage.getItem('cartItems');
    return stored ? JSON.parse(stored) : [];
  });

  // Keep localStorage in sync when cartItems change
  useEffect(() => {
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
  }, [cartItems]);
  const [searchTerm, setSearchTerm] = useState('');

  // ...existing code...

  // Filter products based on category
  // Filter and sort products from backend
  const getFilteredProducts = useCallback(() => {
    let filtered = products;
    if (activeCategory !== 'all') {
      filtered = filtered.filter(product => product.category === activeCategory);
    }
    if (searchTerm.trim() !== '') {
      const term = searchTerm.trim().toLowerCase();
      filtered = filtered.filter(product =>
        (product.name && product.name.toLowerCase().includes(term)) ||
        (product.description && product.description.toLowerCase().includes(term)) ||
        (product.brand && product.brand.toLowerCase().includes(term))
      );
    }
    // Add sorting logic if needed
    return filtered;
  }, [products, activeCategory, searchTerm]);

  // Fetch products from backend
  useEffect(() => {
    setLoading(true);
    fetchProducts()
      .then(data => {
        setProducts(data);
        // Generate categories with counts
        const cats = Array.from(new Set(data.map(p => p.category))).filter(Boolean);
        const catCounts = cats.reduce((acc, cat) => {
          acc[cat] = data.filter(p => p.category === cat).length;
          return acc;
        }, {});
        const allCount = data.length;
        setCategories([
          { id: 'all', name: 'All Products', icon: 'Grid3X3', count: allCount },
          ...cats.map(c => ({ id: c, name: c.charAt(0).toUpperCase() + c.slice(1), icon: 'Box', count: catCounts[c] }))
        ]);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Failed to fetch products:', err);
        setLoading(false);
      });
  }, []);

  // Handle category change
  const handleCategoryChange = (categoryId) => {
    setActiveCategory(categoryId);
    setPage(1);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  // Handle sort change
  const handleSortChange = (newSortBy) => {
    setSortBy(newSortBy);
    setPage(1);
  };

  // Remove load more logic for now (can be re-added with backend pagination)
  const handleLoadMore = () => {};

  // Handle add to cart
  const handleAddToCart = async (product) => {
    // Add product to cart with selected quantity, or update existing
    const productId = product.productId || product.id;
    const addQuantity = product.quantity || 1;
    setCartItems(prev => {
      const existingItem = prev.find(item => (item.productId || item.id) === productId);
      if (existingItem) {
        return prev.map(item =>
          (item.productId || item.id) === productId
            ? { ...item, quantity: item.quantity + addQuantity }
            : item
        );
      }
      return [...prev, { ...product, id: productId, quantity: addQuantity }];
    });
  };

  // Get total results count
  const totalResults = getFilteredProducts().length;

  return (
    <div className="min-h-screen bg-background">
      <Header />
      {/* Search Input */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 pt-4">
        <input
          type="text"
          placeholder="Search products..."
          value={searchTerm}
          onChange={e => setSearchTerm(e.target.value)}
          onKeyDown={e => {
            if (e.key === 'Enter') {
              e.preventDefault();
            }
          }}
          className="w-full md:w-1/2 border border-border rounded-lg px-4 py-2 mb-2 focus:outline-none focus:ring-2 focus:ring-primary"
        />
      </div>
      {/* Category Tabs */}
      <CategoryTabs
        activeCategory={activeCategory}
        onCategoryChange={handleCategoryChange}
        categories={categories}
      />
      {/* Main Content */}
      <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Sort Dropdown removed */}
        {/* Product Grid */}
        <ProductGrid
          products={getFilteredProducts()}
          loading={loading}
          onAddToCart={handleAddToCart}
        />
        {/* Load More Button */}
        {!loading && products.length > 0 && (
          <LoadMoreButton
            onLoadMore={handleLoadMore}
            loading={loadingMore}
            hasMore={hasMore}
          />
        )}
      </main>
    </div>
  );
};

export default ProductCatalog;