import React, { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import Icon from '../../../components/AppIcon';
import Image from '../../../components/AppImage';

const TestimonialsSection = () => {
  const testimonials = [
    {
      id: 1,
      name: "Alex Rodriguez",
      role: "Gaming Enthusiast",
      avatar: "https://randomuser.me/api/portraits/men/32.jpg",
      rating: 5,
      content: `GadgetHub made building my dream gaming PC incredibly easy. The quotation system is brilliant - I got competitive prices for all components and the delivery was lightning fast. Highly recommend!`,
      purchaseInfo: "RTX 4080 Gaming Build"
    },
    {
      id: 2,
      name: "Sarah Chen",
      role: "Content Creator",
      avatar: "https://randomuser.me/api/portraits/women/44.jpg",
      rating: 5,
      content: `As a content creator, I needed reliable equipment fast. GadgetHub's one-click quotation system saved me hours of research. Got my entire streaming setup at great prices with excellent customer service.`,
      purchaseInfo: "Streaming Setup Package"
    },
    {
      id: 3,
      name: "Michael Thompson",
      role: "IT Professional",
      avatar: "https://randomuser.me/api/portraits/men/56.jpg",
      rating: 5,
      content: `Ordered components for our office workstations. The bulk quotation feature and tracking system made procurement seamless. Quality products, competitive pricing, and professional service throughout.`,
      purchaseInfo: "Office Workstation Components"
    },
    {
      id: 4,
      name: "Emily Davis",
      role: "Student",
      avatar: "https://randomuser.me/api/portraits/women/68.jpg",
      rating: 5,
      content: `Perfect for students on a budget! The quotation system helped me find the best deals for my study setup. Great selection of peripherals and the customer support team was incredibly helpful.`,
      purchaseInfo: "Student Study Setup"
    },
    {
      id: 5,
      name: "David Park",
      role: "Small Business Owner",
      avatar: "https://randomuser.me/api/portraits/men/78.jpg",
      rating: 5,
      content: `GadgetHub has been our go-to supplier for tech equipment. The quotation tracking and purchase history features make managing our tech inventory effortless. Reliable partner for our business needs.`,
      purchaseInfo: "Business Tech Equipment"
    },
    {
      id: 6,
      name: "Lisa Wang",
      role: "Graphic Designer",
      avatar: "https://randomuser.me/api/portraits/women/25.jpg",
      rating: 5,
      content: `Needed high-performance components for design work. The detailed product specifications and quick quotation responses helped me make informed decisions. Excellent quality and fast shipping!`,
      purchaseInfo: "Design Workstation Build"
    }
  ];

  const [currentIndex, setCurrentIndex] = useState(0);
  const [isAutoPlaying, setIsAutoPlaying] = useState(true);

  useEffect(() => {
    if (!isAutoPlaying) return;
    
    const timer = setInterval(() => {
      setCurrentIndex((prev) => (prev + 1) % Math.ceil(testimonials.length / 3));
    }, 4000);

    return () => clearInterval(timer);
  }, [isAutoPlaying, testimonials.length]);

  const renderStars = (rating) => {
    return Array.from({ length: 5 }, (_, index) => (
      <Icon
        key={index}
        name="Star"
        size={16}
        className={`${
          index < rating ? 'text-yellow-400 fill-current' : 'text-gray-600'
        }`}
      />
    ));
  };

  const visibleTestimonials = testimonials.slice(currentIndex * 3, (currentIndex + 1) * 3);

  return (
    <section className="py-20 bg-gray-800">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        {/* Header */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.6 }}
          viewport={{ once: true }}
          className="text-center mb-16"
        >
          <h2 className="text-4xl font-bold text-white mb-4">
            What Our Customers Say
          </h2>
          <p className="text-xl text-gray-300 max-w-2xl mx-auto">
            Join thousands of satisfied customers who trust GadgetHub for their tech needs
          </p>
          
          {/* Trust Indicators */}
          <div className="flex items-center justify-center space-x-8 mt-8">
            <div className="flex items-center space-x-2">
              <div className="flex">
                {renderStars(5)}
              </div>
              <span className="text-gray-300 font-medium">4.9/5 Rating</span>
            </div>
            <div className="flex items-center space-x-2">
              <Icon name="Users" size={20} className="text-blue-400" />
              <span className="text-gray-300 font-medium">10,000+ Reviews</span>
            </div>
            <div className="flex items-center space-x-2">
              <Icon name="Shield" size={20} className="text-green-400" />
              <span className="text-gray-300 font-medium">Verified Purchases</span>
            </div>
          </div>
        </motion.div>

        {/* Testimonials Grid */}
        <div className="relative">
          <motion.div
            key={currentIndex}
            initial={{ opacity: 0, x: 50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.6 }}
            className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8"
            onMouseEnter={() => setIsAutoPlaying(false)}
            onMouseLeave={() => setIsAutoPlaying(true)}
          >
            {visibleTestimonials.map((testimonial, index) => (
              <motion.div
                key={testimonial.id}
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.5, delay: index * 0.1 }}
                className="bg-gray-700 rounded-xl p-6 shadow-lg hover:shadow-xl transition-all duration-300 hover:-translate-y-1"
              >
                {/* Rating */}
                <div className="flex items-center space-x-1 mb-4">
                  {renderStars(testimonial.rating)}
                </div>

                {/* Content */}
                <blockquote className="text-gray-300 leading-relaxed mb-6">
                  "{testimonial.content}"
                </blockquote>

                {/* Purchase Info */}
                <div className="bg-gray-600 rounded-lg px-3 py-2 mb-4">
                  <div className="flex items-center space-x-2">
                    <Icon name="Package" size={14} className="text-blue-400" />
                    <span className="text-sm text-gray-300">{testimonial.purchaseInfo}</span>
                  </div>
                </div>

                {/* Author */}
                <div className="flex items-center space-x-3">
                  <div className="w-12 h-12 rounded-full overflow-hidden">
                    <Image
                      src={testimonial.avatar}
                      alt={testimonial.name}
                      className="w-full h-full object-cover"
                    />
                  </div>
                  <div>
                    <div className="font-semibold text-white">{testimonial.name}</div>
                    <div className="text-sm text-gray-400">{testimonial.role}</div>
                  </div>
                  <div className="ml-auto">
                    <Icon name="Quote" size={20} className="text-blue-400 opacity-50" />
                  </div>
                </div>
              </motion.div>
            ))}
          </motion.div>

          {/* Navigation */}
          <div className="flex items-center justify-center space-x-4 mt-12">
            <button
              onClick={() => setCurrentIndex(Math.max(0, currentIndex - 1))}
              disabled={currentIndex === 0}
              className="p-2 rounded-full bg-gray-700 text-white hover:bg-gray-600 disabled:opacity-50 disabled:cursor-not-allowed transition-all duration-200"
            >
              <Icon name="ChevronLeft" size={20} />
            </button>

            <div className="flex space-x-2">
              {Array.from({ length: Math.ceil(testimonials.length / 3) }, (_, index) => (
                <button
                  key={index}
                  onClick={() => setCurrentIndex(index)}
                  className={`w-3 h-3 rounded-full transition-all duration-300 ${
                    index === currentIndex 
                      ? 'bg-blue-400 scale-125' : 'bg-gray-600 hover:bg-gray-500'
                  }`}
                />
              ))}
            </div>

            <button
              onClick={() => setCurrentIndex(Math.min(Math.ceil(testimonials.length / 3) - 1, currentIndex + 1))}
              disabled={currentIndex === Math.ceil(testimonials.length / 3) - 1}
              className="p-2 rounded-full bg-gray-700 text-white hover:bg-gray-600 disabled:opacity-50 disabled:cursor-not-allowed transition-all duration-200"
            >
              <Icon name="ChevronRight" size={20} />
            </button>
          </div>
        </div>
      </div>
    </section>
  );
};

export default TestimonialsSection;