import React from 'react';
import { Helmet } from 'react-helmet';
import Header from '../../components/ui/Header';
import RegistrationForm from './components/RegistrationForm';
import RegistrationBenefits from './components/RegistrationBenefits';
import SecurityFeatures from './components/SecurityFeatures';

const RegistrationScreen = () => {
  return (
    <>
      <Helmet>
        <title>Create Account - GadgetHub | Join Thousands of Tech Enthusiasts</title>
        <meta name="description" content="Create your GadgetHub account to access exclusive tech deals, quotation system, and personalized shopping experience for PC components and gadgets." />
        <meta name="keywords" content="register, signup, create account, GadgetHub, tech, PC components, gadgets" />
      </Helmet>

      <div className="min-h-screen bg-background">
        <Header />
        
        <main className="pt-16">
          {/* Hero Section */}
          <section className="bg-gradient-to-br from-primary/5 via-background to-secondary/5 py-12 lg:py-16">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
              <div className="text-center mb-8">
                <h1 className="text-4xl lg:text-5xl font-bold text-foreground mb-4">
                  Join the GadgetHub Community
                </h1>
                <p className="text-xl text-muted-foreground max-w-2xl mx-auto">
                  Create your account and discover a smarter way to shop for PC components and tech gadgets
                </p>
              </div>
            </div>
          </section>

          {/* Main Content */}
          <section className="py-12 lg:py-16">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
              <div className="flex justify-center items-center min-h-[400px]">
                <div className="w-full max-w-md">
                  <RegistrationForm />
                </div>
              </div>
              {/* Security Features (optional, hidden for now) */}
            </div>
          </section>

          {/* Call to Action Section */}
          <section className="bg-gradient-to-r from-primary/10 to-secondary/10 py-12 border-t border-border/30">
            <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
              <h2 className="text-2xl lg:text-3xl font-bold text-foreground mb-4">
                Ready to Start Your Tech Journey?
              </h2>
              <p className="text-muted-foreground mb-6 max-w-2xl mx-auto">
                Join thousands of satisfied customers who trust GadgetHub for their PC components and gadget needs. 
                Experience the convenience of our quotation-based shopping system today.
              </p>
              <div className="flex flex-col sm:flex-row items-center justify-center space-y-4 sm:space-y-0 sm:space-x-6">
                <div className="flex items-center space-x-2 text-sm text-muted-foreground">
                  <div className="w-2 h-2 bg-green-500 rounded-full"></div>
                  <span>Free to join</span>
                </div>
                <div className="flex items-center space-x-2 text-sm text-muted-foreground">
                  <div className="w-2 h-2 bg-blue-500 rounded-full"></div>
                  <span>Instant access</span>
                </div>
                <div className="flex items-center space-x-2 text-sm text-muted-foreground">
                  <div className="w-2 h-2 bg-purple-500 rounded-full"></div>
                  <span>No hidden fees</span>
                </div>
              </div>
            </div>
          </section>
        </main>

        {/* Footer */}
        <footer className="bg-card border-t border-border py-8">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="text-center text-muted-foreground text-sm">
              <p>&copy; {new Date().getFullYear()} GadgetHub. All rights reserved.</p>
              <p className="mt-2">
                By creating an account, you agree to our Terms of Service and Privacy Policy.
              </p>
            </div>
          </div>
        </footer>
      </div>
    </>
  );
};

export default RegistrationScreen;