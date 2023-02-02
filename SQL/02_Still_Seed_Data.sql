USE [Still]
GO
set identity_insert [UserProfile] ON
insert into "UserProfile" ("Id", "FirebaseUserId", "Name", "Email") values 
(1, 'rWeB1hRRqZcizW5IubcWzbbZFqb2', 'Jane Doe', 'jane@email.com'),
(2, 'GVxswe7oO4by6WKDi3aYKlMavHk1', 'Bob Smith', 'bob@email.com'),
(3, 'VGKUM7xxDARNw8KfA8JFKGeplST2', 'Jill Smith', 'jill@email.com'),
(4, 'K7tLhxacFmYtHNwcy24fTG1oZq23', 'John Doe', 'john@email.com');
set identity_insert [UserProfile] off

set identity_insert [Picture] ON
insert into "Picture" ("Id", "UserProfileId", "Description", "DateCreated", "PictureLocation") values 
(1, 2, 'Carnitas tacos', '2022-12-01', 'https://cdn.whatsgabycooking.com/wp-content/uploads/2019/03/All-Clad-__-Carnitas-Tacos-580x870-1.jpg'),
(2, 2, 'Al Pastor tacos', '2022-11-06', 'https://keviniscooking.com/wp-content/uploads/2017/04/Tacos-Al-Pastor-square.jpg'),
(3, 2, NULL, '2022-11-16', 'https://pinaenlacocina.com/wp-content/uploads/2016/02/18300998_1489141661160019_2627876844777760717_n-500x500.jpg'),
(4, 2, 'Carne asada tacos', '2022-12-26', 'https://keviniscooking.com/wp-content/uploads/2021/03/Carne-Asada-Tacos-2-500x375.jpg'),
(5, 1, 'Baked potato. A classic', '2022-12-27', 'https://fedandfit.com/wp-content/uploads/2021/07/210712_Baked-Potato-10-500x500.jpg'),
(6, 1, 'Taziki''s potatoes. So good!', '2022-12-28', 'https://images.squarespace-cdn.com/content/v1/5d8b8feac0ab5665724d7a2b/1577131753490-5X60EQSH1XPV0CBHYTAK/HealthySides004.jpg'),
(7, 1, 'Pan-fried potatoes. So simple, so delicious.', '2022-12-30', 'https://www.foxvalleyfoodie.com/wp-content/uploads/2019/05/southern-fried-potatoes-recipe.jpg'),
(8, 1, 'Mashed potatoes are life', '2023-02-01', 'https://www.littlebroken.com/wp-content/uploads/2020/11/Red-Skin-Mashed-Potatoes-Recipe-25.jpg'),
(9, 1, 'Potato wedges. Like fries with max potato power.', '2022-12-29', 'https://www.thedinnerbite.com/wp-content/uploads/2019/11/oven-baked-crispy-potato-wedges-img-16.jpg'),
(10, 3, 'Tricolor', '2022-12-30', 'https://cdn.shopify.com/s/files/1/0440/6676/7015/products/keycap.jpg?v=1651735278'),
(11, 3, 'Blue two-tone', '2022-12-31', 'https://m.media-amazon.com/images/I/61YOSdh9TcL.jpg'),
(12, 3, 'Rainbow!.', '2022-12-30', 'http://cdn.shopify.com/s/files/1/0284/9806/7555/products/611D9900-153D-4BDE-9A59-AC7A972D2F1E_1200x1200.jpg?v=1672253333');
set identity_insert [Picture] off

set identity_insert  [Page] ON
insert into "Page" ("Id", "UserProfileId", "Title", "Description", "DateCreated") values 
(1, 2, 'Tacos!', 'These are tacos for no reason other than that I like tacos.', '2022-12-27'),
(2, 1, 'Po-Ta-Toes', 'My favorite ways to eat potatoes (hint: there are no wrong ways).', '2023-02-01'),
(3, 3, 'Keyboard Keycaps', 'Some keycaps I like right now', '2023-02-01');
set identity_insert [Page] off

set identity_insert [PagePicture] ON
insert into "PagePicture" ("Id", "PageId", "PictureId", "Description") values 
(1, 1, 1, NULL),
(2, 1, 2, 'My favorites!'),
(3, 1, 3, NULL),
(4, 1, 4, NULL),
(5, 2, 5, NULL),
(6, 2, 6, 'Some of my favorite restaurant potatoes, Taziki''s'),
(7, 2, 7, 'Okay, but does it get any better?'),
(8, 2, 8, 'Any day, anytime'),
(9, 2, 9, NULL),
(10, 3, 10, NULL),
(11, 3, 11, NULL),
(12, 3, 12, 'All the colors!');
set identity_insert [PagePicture] off