import React, { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import Button from '../../../components/ui/Button';
import Icon from '../../../components/AppIcon';
import Image from '../../../components/AppImage';

const PromotionalBanner = () => {
  const promotions = [
    {
      id: 1,
      title: "New RTX 4090 Series",
      subtitle: "Ultimate Gaming Performance",
      description: "Experience next-gen gaming with the latest NVIDIA RTX 4090 graphics cards. Ray tracing, DLSS 3, and unmatched 4K performance.",
      image: "https://images.unsplash.com/photo-1591488320449-011701bb6704?ixlib=rb-4.0.3&auto=format&fit=crop&w=1200&q=80",
      badge: "New Arrival",
      ctaText: "Explore Now",
      gradient: "from-green-500 to-emerald-600"
    },
    {
      id: 2,
      title: "Gaming Peripherals Sale",
      subtitle: "Up to 40% Off",
      description: "Upgrade your gaming setup with premium keyboards, mice, and headsets from top brands. Limited time offer!",
      image: "https://images.unsplash.com/photo-1541140532154-b024d705b90a?ixlib=rb-4.0.3&auto=format&fit=crop&w=1200&q=80",
      badge: "Hot Deal",
      ctaText: "Shop Sale",
      gradient: "from-red-500 to-pink-600"
    }
  ];

  const [currentPromo, setCurrentPromo] = React.useState(0);

  React.useEffect(() => {
    const timer = setInterval(() => {
      setCurrentPromo((prev) => (prev + 1) % promotions.length);
    }, 5000);

    return () => clearInterval(timer);
  }, []);

  const currentPromotion = promotions[currentPromo];

  return (
    <section className="py-20 bg-gray-900">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <motion.div
          key={currentPromo}
          initial={{ opacity: 0, x: 50 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ duration: 0.6 }}
          className="relative overflow-hidden rounded-2xl bg-gradient-to-r from-gray-800 to-gray-700 shadow-2xl"
        >
          <div className="grid lg:grid-cols-2 gap-0 min-h-[400px]">
            {/* Content Side */}
            <div className="flex flex-col justify-center p-8 lg:p-12 relative z-10">
              {/* Badge */}
              <motion.div
                initial={{ scale: 0 }}
                animate={{ scale: 1 }}
                transition={{ duration: 0.5, delay: 0.2 }}
                className={`inline-flex items-center px-4 py-2 rounded-full bg-gradient-to-r ${currentPromotion.gradient} text-white text-sm font-semibold mb-6 w-fit`}
              >
                <Icon name="Zap" size={16} className="mr-2" />
                {currentPromotion.badge}
              </motion.div>

              {/* Title */}
              <motion.h2
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, delay: 0.3 }}
                className="text-4xl lg:text-5xl font-bold text-white mb-4 leading-tight"
              >
                {currentPromotion.title}
              </motion.h2>

              {/* Subtitle */}
              <motion.p
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, delay: 0.4 }}
                className="text-2xl text-blue-400 font-semibold mb-6"
              >
                {currentPromotion.subtitle}
              </motion.p>

              {/* Description */}
              <motion.p
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, delay: 0.5 }}
                className="text-gray-300 text-lg leading-relaxed mb-8 max-w-md"
              >
                {currentPromotion.description}
              </motion.p>

              {/* CTA Button */}
              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, delay: 0.6 }}
                className="flex flex-col sm:flex-row gap-4"
              >
                <Button
                  variant="default"
                  size="lg"
                  iconName="ArrowRight"
                  iconPosition="right"
                  className="w-full sm:w-auto"
                  onClick={() => window.location.href = '/product-catalog'}
                >
                  {currentPromotion.ctaText}
                </Button>
                
                <Button
                  variant="outline"
                  size="lg"
                  iconName="FileText"
                  iconPosition="left"
                  className="w-full sm:w-auto border-gray-500 text-gray-300 hover:bg-gray-600"
                >
                  Get Quote
                </Button>
              </motion.div>
            </div>

            {/* Image Side */}
            <div className="relative overflow-hidden lg:rounded-r-2xl">
              <Image
                src={currentPromotion.image}
                alt={currentPromotion.title}
                className="w-full h-full object-cover"
              />
              <div className={`absolute inset-0 bg-gradient-to-l ${currentPromotion.gradient} opacity-20`}></div>
              
              {/* Floating Elements */}
              <motion.div
                animate={{ y: [0, -10, 0] }}
                transition={{ duration: 3, repeat: Infinity }}
                className="absolute top-8 right-8 w-16 h-16 bg-white/10 backdrop-blur-sm rounded-full flex items-center justify-center"
              >
                <Icon name="Sparkles" size={24} color="white" />
              </motion.div>
            </div>
          </div>

          {/* Promotion Indicators */}
          <div className="absolute bottom-6 left-8 flex space-x-2">
            {promotions.map((_, index) => (
              <button
                key={index}
                onClick={() => setCurrentPromo(index)}
                className={`w-3 h-3 rounded-full transition-all duration-300 ${
                  index === currentPromo 
                    ? 'bg-white scale-125' : 'bg-white/40 hover:bg-white/60'
                }`}
              />
            ))}
          </div>

          {/* Background Pattern */}
          <div className="absolute inset-0 opacity-5">
            <div className="absolute inset-0 bg-[url('data:image/svg+xml,%3Csvg%20width%3D%2260%22%20height%3D%2260%22%20viewBox%3D%220%200%2060%2060%22%20xmlns%3D%22http%3A//www.w3.org/2000/svg%22%3E%3Cg%20fill%3D%22none%22%20fill-rule%3D%22evenodd%22%3E%3Cg%20fill%3D%22%23ffffff%22%20fill-opacity%3D%221%22%3E%3Ccircle%20cx%3D%227%22%20cy%3D%227%22%20r%3D%221%22/%3E%3C/g%3E%3C/g%3E%3C/svg%3E')]"></div>
          </div>
        </motion.div>
      </div>
    </section>
  );
};

export default PromotionalBanner;