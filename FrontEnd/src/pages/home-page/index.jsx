import React, { useEffect } from 'react';
import { Helmet } from 'react-helmet';
import Header from '../../components/ui/Header';
import HeroSection from './components/HeroSection';
import CategoryGrid from './components/CategoryGrid';
import PromotionalBanner from './components/PromotionalBanner';
import TestimonialsSection from './components/TestimonialsSection';
import CompanyOverview from './components/CompanyOverview';
import Footer from './components/Footer';

const HomePage = () => {
  useEffect(() => {
    // Scroll to top when component mounts
    window.scrollTo(0, 0);
  }, []);

  return (
    <>
      <Helmet>
        <title>GadgetHub - Your Ultimate Tech Destination | PC Components & Gadgets</title>
        <meta 
          name="description" 
          content="Discover premium PC components, gaming peripherals, and cutting-edge gadgets at GadgetHub. Get instant quotations, track orders, and build your dream tech setup with our comprehensive catalog." 
        />
        <meta name="keywords" content="PC components, graphics cards, gaming peripherals, tech gadgets, computer hardware, quotation system" />
        <meta property="og:title" content="GadgetHub - Your Ultimate Tech Destination" />
        <meta property="og:description" content="Premium PC components and gadgets with instant quotation system" />
        <meta property="og:type" content="website" />
        <link rel="canonical" href="/home-page" />
      </Helmet>

      <div className="min-h-screen bg-gray-900">
        {/* Header */}
        <Header />
        
        {/* Main Content */}
        <main className="pt-16">
          {/* Hero Section */}
          <HeroSection />
          
          {/* Category Grid */}
          <CategoryGrid />
          
          {/* Promotional Banner */}
          <PromotionalBanner />
          
          {/* Testimonials Section */}
          <TestimonialsSection />
          
          {/* Company Overview */}
          <CompanyOverview />
        </main>
        
        {/* Footer */}
        <Footer />
      </div>
    </>
  );
};

export default HomePage;