
import React, { useState, useEffect } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import ProductImageGallery from './components/ProductImageGallery';
import ProductInfo from './components/ProductInfo';
import ProductSpecifications from './components/ProductSpecifications';
import CustomerReviews from './components/CustomerReviews';
import RelatedProducts from './components/RelatedProducts';
import BreadcrumbNavigation from './components/BreadcrumbNavigation';
import { fetchProductById } from '../../utils/api';

const ProductDetail = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [quotationItems, setQuotationItems] = useState([]);



  useEffect(() => {
    const productId = searchParams.get('id');
    if (!productId) {
      navigate('/product-catalog');
      return;
    }
    setLoading(true);
    fetchProductById(productId)
      .then((data) => {
        setProduct(data);
        setLoading(false);
      })
      .catch(() => {
        setProduct(null);
        setLoading(false);
        navigate('/product-catalog');
      });

    // Load quotation items from localStorage
    const savedQuotationItems = localStorage.getItem('quotationItems');
    if (savedQuotationItems) {
      setQuotationItems(JSON.parse(savedQuotationItems));
    }
  }, [searchParams, navigate]);

  const handleAddToQuote = async (productToAdd, quantity) => {
    const newItem = {
      id: Date.now(),
      productId: productToAdd.id,
      name: productToAdd.name,
      price: productToAdd.price,
      quantity: quantity,
      image: productToAdd.images ? productToAdd.images[0] : productToAdd.image,
      addedAt: new Date().toISOString()
    };

    const updatedItems = [...quotationItems, newItem];
    setQuotationItems(updatedItems);
    localStorage.setItem('quotationItems', JSON.stringify(updatedItems));

    // Show success notification (you can implement a toast notification here)
    console.log('Added to quotation:', newItem);
  };


  if (loading) {
    return (
      <div className="min-h-screen bg-background pt-20">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          <div className="animate-pulse">
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-12">
              <div className="aspect-square bg-muted rounded-lg"></div>
              <div className="space-y-4">
                <div className="h-8 bg-muted rounded w-3/4"></div>
                <div className="h-4 bg-muted rounded w-1/2"></div>
                <div className="h-32 bg-muted rounded"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  if (!product) {
    return (
      <div className="min-h-screen bg-background pt-20">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          <div className="text-center">
            <h1 className="text-2xl font-bold text-foreground mb-4">Product Not Found</h1>
            <p className="text-muted-foreground mb-8">The product you're looking for doesn't exist.</p>
            <button
              onClick={() => navigate('/product-catalog')}
              className="bg-primary text-primary-foreground px-6 py-3 rounded-lg hover:bg-primary/90 transition-colors duration-150"
            >
              Browse Products
            </button>
          </div>
        </div>
      </div>
    );
  }

  const breadcrumbs = [
    { label: "Home", path: "/home-page" },
    { label: "Products", path: "/product-catalog" },
    { label: product.category, path: `/product-catalog?category=${encodeURIComponent(product.category || '')}` },
    { label: product.name }
  ];

  return (
    <>
      <Helmet>
        <title>{product.name} - GadgetHub</title>
        <meta name="description" content={product.description} />
        <meta property="og:title" content={`${product.name} - GadgetHub`} />
        <meta property="og:description" content={product.description} />
        <meta property="og:image" content={product.images && product.images[0]} />
        <meta property="og:type" content="product" />
        <meta name="twitter:card" content="summary_large_image" />
        <meta name="twitter:title" content={`${product.name} - GadgetHub`} />
        <meta name="twitter:description" content={product.description} />
        <meta name="twitter:image" content={product.images && product.images[0]} />
      </Helmet>

      <div className="min-h-screen bg-background pt-20">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          {/* Breadcrumb Navigation */}
          <BreadcrumbNavigation breadcrumbs={breadcrumbs} />

          {/* Product Overview */}
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-12">
            {/* Product Images */}
            <div className="lg:sticky lg:top-24 lg:self-start">
            <ProductImageGallery
              images={
                Array.isArray(product.images) && product.images.length > 0
                  ? product.images
                  : product.url
                    ? [product.url]
                    : product.image
                      ? [product.image]
                      : []
              }
              productName={product.name}
            />
            </div>

            {/* Product Information */}
            <div>
              <ProductInfo 
                product={product} 
                onAddToQuote={handleAddToQuote} 
              />
            </div>
          </div>

          {/* Product Specifications */}
          <div className="mb-12">
            <ProductSpecifications specifications={product.specifications || {}} />
            {!product.specifications || Object.keys(product.specifications).length === 0 ? (
              <div className="text-muted-foreground mt-2">No specifications available.</div>
            ) : null}
          </div>

          {/* Customer Reviews removed as requested */}

          {/* Related Products */}
          <div className="mb-12">
            <RelatedProducts 
              products={[]} // No related products yet
              onAddToQuote={handleAddToQuote}
            />
          </div>
        </div>
      </div>
    </>
  );
};

export default ProductDetail;