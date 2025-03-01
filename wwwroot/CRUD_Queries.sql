-- select * from tbl_admin;
-- select * from tbl_category;
-- select * from tbl_product;
-- select * from tbl_customer;
-- select * from tbl_cartitem;
-- select * from tbl_orderitem;
select * from tbl_order;

-- delete
-- delete from tbl_customer where customer_id = 1006;
-- delete from tbl_customer where admin_id = 5;
-- delete from tbl_orderitem where orderitem_id = 2;
-- delete from tbl_order where order_id = 4;

-- update
-- update tbl_customer
-- set customer_password = '123456'
-- where customer_id = 1005;

-- insert
-- INSERT INTO tbl_customer (
--     customer_name, customer_phone, customer_email, 
--     customer_password, customer_address, customer_city, 
--     customer_country, customer_pincode  
-- ) VALUES
-- ('Atharv Noob', '123456789', 'noob@example.com', 'noobpass', 'Dgs Virar MH', 'Mumbai', 'India', '401305'),
-- ('Rohan Paw', '234567891', 'rohan@example.com', 'rohanpass', 'Bapu Nagar ', 'Ahmedabad', 'India', '501405'),
-- ('Ashu Hat', '345678912', 'ashu@example.com', 'ashupass', 'Ashu House', 'Palghar', 'India', '411205'),
-- ('Azz Bhu', '8765432108', 'azz@example.de', 'azzpass', '30 Hauptstr', 'Berlin', 'Germany', '10115'),
-- ('Emily Wilson', '3322110099', 'emily.wilson@example.ca', 'emilypass', '15 Queen St', 'Toronto', 'Canada', 'M5H2N2');

-- INSERT INTO tbl_admin
-- (
--      admin_name, admin_email, admin_password  
-- ) 
-- VALUES
-- ('Main Admin', 'admin@gmail.com', '098'),
-- ('Om Admin', 'om@gmail.com', '123');

-- insert into tbl_category
-- (
--     category_name
-- )
-- values
-- ('Nike'),
-- ('Adidas'),
-- ('Puma'),
-- ('New Balance'),
-- ('Reebok'),
-- ('Converse');


-- INSERT INTO tbl_product
-- (
--     product_name, product_price,
--     product_description, product_image, category_id
-- )
-- VALUES
-- ('Nike Air Force 1s', 1999.00, 'Comfortable, durable and timeless.', 'nike-AIR-FORCE-1-07-FRESH.png', 1),
-- ('Adidas Samba 00s', 2499.00, 'Never Out of Style.', 'adidas-campus-00.jpg', 1),
-- ('New Balance 327', 2999.00, 'The 327 is a bold blend of retro and right now unlike anything in your closet.', 'nb-running-shoes.png', 4),
-- ('Converse RunStar', 3499.00, 'Bold, exaggerated midsole details bring can''t-miss style to your favorite platforms.', 'converse-chunky.jpg', 6),
-- ('Puma EasyRider', 2799.00, 'Bringing a touch of easy, retro-style to everyday looks.', 'puma-Easy-Rider-Vintage.jpg', 3),
-- ('Reebok Engine A', 3299.00, 'Equipped with ERS (Energy Return System) technology, this lightweight, high-flying, tough basketball shoe is perfect for real hoopers.', 'reebok-classic-reflector.png', 5),
-- ('Nike Blazer 77', 3799.00, 'The Nike Blazer Mid ''77 Vintage—classic since the beginning.', 'nike-BLAZER-MID-77.png', 1),
-- ('New Balance 550', 2399.00, 'Originally launched in the 1980s, this retro basketball silhouette has become a staple for sneaker enthusiasts.', 'nb-550.png', 4),
-- ('Converse Chuck 70', 1799.00, 'The best-ever gets a seasonal makeover with premium canvas in archival color.', 'converse-chuck-07.jpg', 6),
-- ('Puma NightRunner', 1299.00, 'A running shoe that is great for training and daily wear.', 'puma-night-runner-V3.jpg', 3),
-- ('Reebok Smash Edge', 1199.00, 'Old-school design meets modern comfort and a clean, elevated aesthetic to these Reebok Smash Edge shoes.', 'reebok-sneaker.png', 5),
-- ('Adidas CrazyChaos', 2699.00, 'Add some retro appeal to your sneaker game.', 'adidas-Crazychaos-2000-Shoes-White_.jpg', 2);


-- truncate
-- BEGIN TRANSACTION;
-- truncate table tbl_category;
-- COMMIT;

-- drop a single column
-- ALTER TABLE tbl_product
-- DROP COLUMN category_id;

-- DELETE FROM tbl_product WHERE category_id IN (SELECT category_id FROM tbl_category);
-- TRUNCATE TABLE tbl_category;