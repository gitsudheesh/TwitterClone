//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using TwitterEntity;
//namespace TwitterService
//{
//    public class TwittterService
//    {
//    }
//    public class TwitterCloneBO--
//    {
//        public int ValidatedUser(string userId, string password)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                var result = db.People.Find(userId);

//                if (result != null)
//                {
//                    if (result.Active)
//                    {
//                        return (result.User_id.ToUpper() == userId.ToUpper() && result.Password == password) ? 1 : 2;
//                    }
//                    else
//                    {
//                        return 3;
//                    }
//                }
//                else
//                {
//                    return 0;
//                }

//            }
//        }

//        public Person GetUserDetails(string userid)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                return db.People.Find(userid);
//            }
//        }

//        public bool CreateAccount(Person item)
//        {
//            try
//            {
//                using (TwitterDBEntities db = new TwitterDBEntities())
//                {
//                    db.People.Add(item);
//                    db.SaveChanges();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public void SaveTweets(Tweet item)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                if (item.tweet_id == 0)
//                {
//                    db.Tweets.Add(item);
//                }
//                else
//                {
//                    Tweet twetObj = db.Tweets.Find(item.tweet_id);
//                    twetObj.message = item.message;
//                    twetObj.created = item.created;
//                }
//                db.SaveChanges();
//            }
//        }

//        public void DeleteTweet(int tweetId)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                db.Tweets.Remove(db.Tweets.Find(tweetId));
//                db.SaveChanges();
//            }
//        }

//        public IEnumerable<Tweet> GetTotalTweetByUser(string id)
//        {
//            TwitterDBEntities db = new TwitterDBEntities();
//            return db.Tweets.Where(x => x.user_id.ToUpper() == id.ToUpper()).OrderByDescending(o => o.created);
//        }

//        public IEnumerable<Tweet> GetAllTweetList(string id)
//        {
//            List<Tweet> tweetList = new List<Tweet>();

//            TwitterDBEntities db = new TwitterDBEntities();

//            var followingList = db.Followings.Where(x => x.User_id.ToUpper() == id.ToUpper())
//                                              .Select(x => x.Following_id).ToList();

//            return db.Tweets.Where(tweet => followingList.Contains(tweet.user_id) || tweet.user_id == id)
//                            .OrderByDescending(o => o.created);

//        }

//        public List<Following> GetAllFollowing(string userid)
//        {
//            TwitterDBEntities db = new TwitterDBEntities();
//            return db.Followings.Where(x => x.User_id.ToUpper() == userid.ToUpper()).ToList();
//        }
//        public List<Following> GetAllFollowers(string userid)
//        {
//            TwitterDBEntities db = new TwitterDBEntities();
//            return db.Followings.Where(x => x.Following_id.ToUpper() == userid.ToUpper()).ToList();
//        }
//        public void DeleteAccount(string userid)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                //db.uspDeleteAccount(userid);
//            }
//        }

//        public void UpdateProfile(Person item)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                Person personObj = db.People.Find(item.User_id);
//                personObj.FullName = item.FullName;
//                personObj.Email = item.Email;
//                personObj.Password = item.Password;

//                db.SaveChanges();
//            }
//        }

//        public List<Person> GetSearchList(string name)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                return db.People.Where(x => x.User_id.StartsWith(name)).ToList();
//            }
//        }

//        public void FollowUser(string userid, string followingId)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                Following obj = new Following();
//                obj.User_id = userid;
//                obj.Following_id = followingId;

//                db.Followings.Add(obj);
//                db.SaveChanges();
//            }
//        }
//        public void UnFollowUser(string userid, string followingId)
//        {
//            using (TwitterDBEntities db = new TwitterDBEntities())
//            {
//                var result = db.Followings.Where(x => x.User_id.ToUpper() == userid.ToUpper() && x.Following_id.ToUpper() == followingId.ToUpper()).FirstOrDefault();
//                db.Followings.Remove(db.Followings.Find(result.Rowid));
//                db.SaveChanges();
//            }
//        }
//    }
//}