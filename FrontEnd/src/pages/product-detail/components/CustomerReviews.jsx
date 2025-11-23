import React, { useState } from 'react';
import Icon from '../../../components/AppIcon';
import Button from '../../../components/ui/Button';

const CustomerReviews = ({ reviews, averageRating, totalReviews }) => {
  const [sortBy, setSortBy] = useState('newest');
  const [showAllReviews, setShowAllReviews] = useState(false);

  const sortedReviews = [...reviews].sort((a, b) => {
    switch (sortBy) {
      case 'newest':
        return new Date(b.date) - new Date(a.date);
      case 'oldest':
        return new Date(a.date) - new Date(b.date);
      case 'highest':
        return b.rating - a.rating;
      case 'lowest':
        return a.rating - b.rating;
      default:
        return 0;
    }
  });

  const displayedReviews = showAllReviews ? sortedReviews : sortedReviews.slice(0, 3);

  const getRatingDistribution = () => {
    const distribution = { 5: 0, 4: 0, 3: 0, 2: 0, 1: 0 };
    reviews.forEach(review => {
      distribution[review.rating]++;
    });
    return distribution;
  };

  const ratingDistribution = getRatingDistribution();

  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  };

  return (
    <div className="bg-card rounded-lg p-6">
      <h2 className="text-xl font-bold text-foreground mb-6 flex items-center">
        <Icon name="MessageSquare" size={20} className="mr-2" />
        Customer Reviews
      </h2>

      {/* Rating Summary */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-8">
        <div className="text-center md:text-left">
          <div className="flex items-center justify-center md:justify-start space-x-2 mb-2">
            <span className="text-4xl font-bold text-foreground">{averageRating}</span>
            <div className="flex">
              {[...Array(5)].map((_, i) => (
                <Icon
                  key={i}
                  name="Star"
                  size={20}
                  className={`${
                    i < Math.floor(averageRating)
                      ? 'text-warning fill-current' :'text-muted'
                  }`}
                />
              ))}
            </div>
          </div>
          <p className="text-muted-foreground">
            Based on {totalReviews} reviews
          </p>
        </div>

        <div className="space-y-2">
          {[5, 4, 3, 2, 1].map((rating) => (
            <div key={rating} className="flex items-center space-x-2">
              <span className="text-sm text-muted-foreground w-8">{rating}</span>
              <Icon name="Star" size={14} className="text-warning fill-current" />
              <div className="flex-1 bg-muted rounded-full h-2">
                <div
                  className="bg-warning h-2 rounded-full transition-all duration-300"
                  style={{
                    width: `${totalReviews > 0 ? (ratingDistribution[rating] / totalReviews) * 100 : 0}%`
                  }}
                />
              </div>
              <span className="text-sm text-muted-foreground w-8">
                {ratingDistribution[rating]}
              </span>
            </div>
          ))}
        </div>
      </div>

      {/* Sort Options */}
      <div className="flex items-center justify-between mb-6">
        <h3 className="text-lg font-semibold text-foreground">
          Reviews ({totalReviews})
        </h3>
        <select
          value={sortBy}
          onChange={(e) => setSortBy(e.target.value)}
          className="bg-input border border-border rounded-lg px-3 py-2 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-ring"
        >
          <option value="newest">Newest First</option>
          <option value="oldest">Oldest First</option>
          <option value="highest">Highest Rating</option>
          <option value="lowest">Lowest Rating</option>
        </select>
      </div>

      {/* Reviews List */}
      <div className="space-y-6">
        {displayedReviews.map((review) => (
          <div key={review.id} className="border-b border-border pb-6 last:border-b-0">
            <div className="flex items-start space-x-4">
              <div className="w-10 h-10 bg-primary rounded-full flex items-center justify-center text-primary-foreground font-semibold">
                {review.author.charAt(0).toUpperCase()}
              </div>
              <div className="flex-1">
                <div className="flex items-center justify-between mb-2">
                  <div>
                    <h4 className="font-semibold text-foreground">{review.author}</h4>
                    <div className="flex items-center space-x-2">
                      <div className="flex">
                        {[...Array(5)].map((_, i) => (
                          <Icon
                            key={i}
                            name="Star"
                            size={14}
                            className={`${
                              i < review.rating
                                ? 'text-warning fill-current' :'text-muted'
                            }`}
                          />
                        ))}
                      </div>
                      <span className="text-sm text-muted-foreground">
                        {formatDate(review.date)}
                      </span>
                    </div>
                  </div>
                  {review.verified && (
                    <div className="flex items-center space-x-1 text-accent text-sm">
                      <Icon name="ShieldCheck" size={14} />
                      <span>Verified Purchase</span>
                    </div>
                  )}
                </div>
                <p className="text-muted-foreground mb-3">{review.comment}</p>
                {review.pros && review.pros.length > 0 && (
                  <div className="mb-2">
                    <span className="text-sm font-medium text-accent">Pros: </span>
                    <span className="text-sm text-muted-foreground">{review.pros.join(', ')}</span>
                  </div>
                )}
                {review.cons && review.cons.length > 0 && (
                  <div className="mb-2">
                    <span className="text-sm font-medium text-error">Cons: </span>
                    <span className="text-sm text-muted-foreground">{review.cons.join(', ')}</span>
                  </div>
                )}
                <div className="flex items-center space-x-4 text-sm text-muted-foreground">
                  <button className="flex items-center space-x-1 hover:text-foreground transition-colors duration-150">
                    <Icon name="ThumbsUp" size={14} />
                    <span>Helpful ({review.helpful})</span>
                  </button>
                  <button className="flex items-center space-x-1 hover:text-foreground transition-colors duration-150">
                    <Icon name="MessageCircle" size={14} />
                    <span>Reply</span>
                  </button>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* Show More Button */}
      {reviews.length > 3 && (
        <div className="text-center mt-6">
          <Button
            variant="outline"
            onClick={() => setShowAllReviews(!showAllReviews)}
            iconName={showAllReviews ? "ChevronUp" : "ChevronDown"}
            iconPosition="right"
          >
            {showAllReviews ? 'Show Less Reviews' : `Show All ${totalReviews} Reviews`}
          </Button>
        </div>
      )}
    </div>
  );
};

export default CustomerReviews;