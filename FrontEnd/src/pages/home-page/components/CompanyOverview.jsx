import React from 'react';
import { motion } from 'framer-motion';
import Icon from '../../../components/AppIcon';
import Image from '../../../components/AppImage';

const CompanyOverview = () => {
  const features = [
    {
      icon: "Shield",
      title: "Trusted Quality",
      description: "All products are sourced directly from authorized manufacturers with full warranty coverage."
    },
    {
      icon: "Zap",
      title: "Fast Delivery",
      description: "Express shipping options available with same-day dispatch for in-stock items."
    },
    {
      icon: "HeadphonesIcon",
      title: "Expert Support",
      description: "24/7 technical support from certified professionals to help with your tech needs."
    },
    {
      icon: "Award",
      title: "Best Prices",
      description: "Competitive pricing with our quotation system ensuring you get the best deals available."
    }
  ];

  const stats = [
    { number: "50,000+", label: "Products Delivered", icon: "Package" },
    { number: "10,000+", label: "Happy Customers", icon: "Users" },
    { number: "500+", label: "Brand Partners", icon: "Handshake" },
    { number: "99.9%", label: "Uptime Guarantee", icon: "Clock" }
  ];

  return (
    <section className="py-20 bg-gray-900">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid lg:grid-cols-2 gap-16 items-center">
          {/* Content Side */}
          <motion.div
            initial={{ opacity: 0, x: -50 }}
            whileInView={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.8 }}
            viewport={{ once: true }}
          >
            <div className="space-y-6">
              <div className="inline-flex items-center px-4 py-2 bg-blue-500/10 border border-blue-500/20 rounded-full">
                <Icon name="Info" size={16} className="text-blue-400 mr-2" />
                <span className="text-blue-400 text-sm font-medium">About GadgetHub</span>
              </div>

              <h2 className="text-4xl lg:text-5xl font-bold text-white leading-tight">
                Your Trusted Partner in 
                <span className="text-transparent bg-clip-text bg-gradient-to-r from-blue-400 to-emerald-400">
                  {" "}Technology
                </span>
              </h2>

              <p className="text-xl text-gray-300 leading-relaxed">
                Since 2020, GadgetHub has been at the forefront of providing premium PC components 
                and cutting-edge gadgets to tech enthusiasts, gamers, and professionals worldwide.
              </p>

              <p className="text-gray-400 leading-relaxed">
                Our innovative quotation-based system revolutionizes how you shop for technology. 
                Instead of fixed pricing, we provide personalized quotes that ensure you get the 
                best possible deals on every purchase. From high-performance graphics cards to 
                essential peripherals, we've got everything you need to build your perfect setup.
              </p>

              {/* Features Grid */}
              <div className="grid sm:grid-cols-2 gap-6 pt-8">
                {features.map((feature, index) => (
                  <motion.div
                    key={index}
                    initial={{ opacity: 0, y: 20 }}
                    whileInView={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.5, delay: index * 0.1 }}
                    viewport={{ once: true }}
                    className="flex items-start space-x-4"
                  >
                    <div className="flex-shrink-0 w-12 h-12 bg-gradient-to-r from-blue-500 to-emerald-500 rounded-lg flex items-center justify-center">
                      <Icon name={feature.icon} size={20} color="white" />
                    </div>
                    <div>
                      <h3 className="text-white font-semibold mb-2">{feature.title}</h3>
                      <p className="text-gray-400 text-sm leading-relaxed">{feature.description}</p>
                    </div>
                  </motion.div>
                ))}
              </div>
            </div>
          </motion.div>

          {/* Visual Side */}
          <motion.div
            initial={{ opacity: 0, x: 50 }}
            whileInView={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.8 }}
            viewport={{ once: true }}
            className="relative"
          >
            {/* Main Image */}
            <div className="relative rounded-2xl overflow-hidden shadow-2xl">
              <Image
                src="https://images.unsplash.com/photo-1560472354-b33ff0c44a43?ixlib=rb-4.0.3&auto=format&fit=crop&w=1200&q=80"
                alt="Modern tech workspace with multiple monitors and components"
                className="w-full h-96 object-cover"
              />
              <div className="absolute inset-0 bg-gradient-to-t from-gray-900/80 via-transparent to-transparent"></div>
              
              {/* Floating Stats Card */}
              <motion.div
                initial={{ opacity: 0, scale: 0.8 }}
                whileInView={{ opacity: 1, scale: 1 }}
                transition={{ duration: 0.6, delay: 0.4 }}
                viewport={{ once: true }}
                className="absolute bottom-6 left-6 right-6 bg-white/10 backdrop-blur-md rounded-xl p-4 border border-white/20"
              >
                <div className="grid grid-cols-2 gap-4">
                  {stats.slice(0, 2).map((stat, index) => (
                    <div key={index} className="text-center">
                      <div className="flex items-center justify-center mb-2">
                        <Icon name={stat.icon} size={16} className="text-blue-400 mr-2" />
                        <span className="text-2xl font-bold text-white">{stat.number}</span>
                      </div>
                      <div className="text-gray-300 text-sm">{stat.label}</div>
                    </div>
                  ))}
                </div>
              </motion.div>
            </div>

            {/* Additional Stats */}
            <motion.div
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.6, delay: 0.6 }}
              viewport={{ once: true }}
              className="grid grid-cols-2 gap-4 mt-6"
            >
              {stats.slice(2).map((stat, index) => (
                <div key={index} className="bg-gray-800 rounded-xl p-6 text-center border border-gray-700">
                  <div className="flex items-center justify-center mb-3">
                    <Icon name={stat.icon} size={20} className="text-emerald-400 mr-2" />
                    <span className="text-2xl font-bold text-white">{stat.number}</span>
                  </div>
                  <div className="text-gray-400 text-sm">{stat.label}</div>
                </div>
              ))}
            </motion.div>

            {/* Decorative Elements */}
            <div className="absolute -top-4 -right-4 w-24 h-24 bg-gradient-to-r from-blue-500 to-emerald-500 rounded-full opacity-20 blur-xl"></div>
            <div className="absolute -bottom-4 -left-4 w-32 h-32 bg-gradient-to-r from-purple-500 to-pink-500 rounded-full opacity-20 blur-xl"></div>
          </motion.div>
        </div>
      </div>
    </section>
  );
};

export default CompanyOverview;