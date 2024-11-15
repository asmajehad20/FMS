PGDMP                         |           asmajehad_fms    11.22    11.22 C    W           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            X           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            Y           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            Z           1262    16531    asmajehad_fms    DATABASE     �   CREATE DATABASE asmajehad_fms WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1256' LC_CTYPE = 'English_United States.1256';
    DROP DATABASE asmajehad_fms;
             postgres    false            �            1259    16807    circlegeofence    TABLE     �   CREATE TABLE public.circlegeofence (
    id bigint NOT NULL,
    geofenceid bigint,
    radius bigint,
    latitude real,
    longitude real
);
 "   DROP TABLE public.circlegeofence;
       public         postgres    false            �            1259    16805    circlegeofence_id_seq    SEQUENCE     ~   CREATE SEQUENCE public.circlegeofence_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public.circlegeofence_id_seq;
       public       postgres    false    207            [           0    0    circlegeofence_id_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE public.circlegeofence_id_seq OWNED BY public.circlegeofence.id;
            public       postgres    false    206            �            1259    16681    driver    TABLE     w   CREATE TABLE public.driver (
    driverid bigint NOT NULL,
    drivername character varying,
    phonenumber bigint
);
    DROP TABLE public.driver;
       public         postgres    false            �            1259    16679    driver_driverid_seq    SEQUENCE     |   CREATE SEQUENCE public.driver_driverid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.driver_driverid_seq;
       public       postgres    false    199            \           0    0    driver_driverid_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.driver_driverid_seq OWNED BY public.driver.driverid;
            public       postgres    false    198            �            1259    16796 	   geofences    TABLE       CREATE TABLE public.geofences (
    geofenceid bigint NOT NULL,
    geofencetype character varying,
    addeddate bigint,
    strockcolor character varying,
    strockopacity real,
    strockweight real,
    fillcolor character varying,
    fillopacity real
);
    DROP TABLE public.geofences;
       public         postgres    false            �            1259    16794    geofences_geofenceid_seq    SEQUENCE     �   CREATE SEQUENCE public.geofences_geofenceid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.geofences_geofenceid_seq;
       public       postgres    false    205            ]           0    0    geofences_geofenceid_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.geofences_geofenceid_seq OWNED BY public.geofences.geofenceid;
            public       postgres    false    204            �            1259    16833    polygongeofence    TABLE     ~   CREATE TABLE public.polygongeofence (
    id bigint NOT NULL,
    geofenceid bigint,
    latitude real,
    longitude real
);
 #   DROP TABLE public.polygongeofence;
       public         postgres    false            �            1259    16831    polygongeofence_id_seq    SEQUENCE        CREATE SEQUENCE public.polygongeofence_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.polygongeofence_id_seq;
       public       postgres    false    211            ^           0    0    polygongeofence_id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.polygongeofence_id_seq OWNED BY public.polygongeofence.id;
            public       postgres    false    210            �            1259    16820    rectanglegeofence    TABLE     �   CREATE TABLE public.rectanglegeofence (
    id bigint NOT NULL,
    geofenceid bigint,
    north real,
    east real,
    west real,
    south real
);
 %   DROP TABLE public.rectanglegeofence;
       public         postgres    false            �            1259    16818    rectanglegeofence_id_seq    SEQUENCE     �   CREATE SEQUENCE public.rectanglegeofence_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.rectanglegeofence_id_seq;
       public       postgres    false    209            _           0    0    rectanglegeofence_id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.rectanglegeofence_id_seq OWNED BY public.rectanglegeofence.id;
            public       postgres    false    208            �            1259    16779    routehistory    TABLE       CREATE TABLE public.routehistory (
    routehistoryid bigint NOT NULL,
    vehicleid bigint,
    vehicledirection integer,
    status character(1),
    vehiclespeed character varying,
    epoch bigint,
    address character varying,
    latitude real,
    longitude real
);
     DROP TABLE public.routehistory;
       public         postgres    false            �            1259    16777    routehistory_routehistoryid_seq    SEQUENCE     �   CREATE SEQUENCE public.routehistory_routehistoryid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 6   DROP SEQUENCE public.routehistory_routehistoryid_seq;
       public       postgres    false    203            `           0    0    routehistory_routehistoryid_seq    SEQUENCE OWNED BY     c   ALTER SEQUENCE public.routehistory_routehistoryid_seq OWNED BY public.routehistory.routehistoryid;
            public       postgres    false    202            �            1259    16670    vehicles    TABLE     }   CREATE TABLE public.vehicles (
    vehicleid bigint NOT NULL,
    vehiclenumber bigint,
    vehicletype character varying
);
    DROP TABLE public.vehicles;
       public         postgres    false            �            1259    16668    vehicles_vehicleid_seq    SEQUENCE        CREATE SEQUENCE public.vehicles_vehicleid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.vehicles_vehicleid_seq;
       public       postgres    false    197            a           0    0    vehicles_vehicleid_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.vehicles_vehicleid_seq OWNED BY public.vehicles.vehicleid;
            public       postgres    false    196            �            1259    16692    vehiclesinformations    TABLE     �   CREATE TABLE public.vehiclesinformations (
    id bigint NOT NULL,
    vehicleid bigint,
    driverid bigint,
    vehiclemake character varying,
    vehiclemodel character varying,
    purchasedate bigint
);
 (   DROP TABLE public.vehiclesinformations;
       public         postgres    false            �            1259    16690    vehiclesinformations_id_seq    SEQUENCE     �   CREATE SEQUENCE public.vehiclesinformations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public.vehiclesinformations_id_seq;
       public       postgres    false    201            b           0    0    vehiclesinformations_id_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public.vehiclesinformations_id_seq OWNED BY public.vehiclesinformations.id;
            public       postgres    false    200            �
           2604    16810    circlegeofence id    DEFAULT     v   ALTER TABLE ONLY public.circlegeofence ALTER COLUMN id SET DEFAULT nextval('public.circlegeofence_id_seq'::regclass);
 @   ALTER TABLE public.circlegeofence ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    207    206    207            �
           2604    16684    driver driverid    DEFAULT     r   ALTER TABLE ONLY public.driver ALTER COLUMN driverid SET DEFAULT nextval('public.driver_driverid_seq'::regclass);
 >   ALTER TABLE public.driver ALTER COLUMN driverid DROP DEFAULT;
       public       postgres    false    199    198    199            �
           2604    16799    geofences geofenceid    DEFAULT     |   ALTER TABLE ONLY public.geofences ALTER COLUMN geofenceid SET DEFAULT nextval('public.geofences_geofenceid_seq'::regclass);
 C   ALTER TABLE public.geofences ALTER COLUMN geofenceid DROP DEFAULT;
       public       postgres    false    204    205    205            �
           2604    16836    polygongeofence id    DEFAULT     x   ALTER TABLE ONLY public.polygongeofence ALTER COLUMN id SET DEFAULT nextval('public.polygongeofence_id_seq'::regclass);
 A   ALTER TABLE public.polygongeofence ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    210    211    211            �
           2604    16823    rectanglegeofence id    DEFAULT     |   ALTER TABLE ONLY public.rectanglegeofence ALTER COLUMN id SET DEFAULT nextval('public.rectanglegeofence_id_seq'::regclass);
 C   ALTER TABLE public.rectanglegeofence ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    209    208    209            �
           2604    16782    routehistory routehistoryid    DEFAULT     �   ALTER TABLE ONLY public.routehistory ALTER COLUMN routehistoryid SET DEFAULT nextval('public.routehistory_routehistoryid_seq'::regclass);
 J   ALTER TABLE public.routehistory ALTER COLUMN routehistoryid DROP DEFAULT;
       public       postgres    false    203    202    203            �
           2604    16673    vehicles vehicleid    DEFAULT     x   ALTER TABLE ONLY public.vehicles ALTER COLUMN vehicleid SET DEFAULT nextval('public.vehicles_vehicleid_seq'::regclass);
 A   ALTER TABLE public.vehicles ALTER COLUMN vehicleid DROP DEFAULT;
       public       postgres    false    197    196    197            �
           2604    16695    vehiclesinformations id    DEFAULT     �   ALTER TABLE ONLY public.vehiclesinformations ALTER COLUMN id SET DEFAULT nextval('public.vehiclesinformations_id_seq'::regclass);
 F   ALTER TABLE public.vehiclesinformations ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    201    200    201            P          0    16807    circlegeofence 
   TABLE DATA               U   COPY public.circlegeofence (id, geofenceid, radius, latitude, longitude) FROM stdin;
    public       postgres    false    207   +O       H          0    16681    driver 
   TABLE DATA               C   COPY public.driver (driverid, drivername, phonenumber) FROM stdin;
    public       postgres    false    199   �O       N          0    16796 	   geofences 
   TABLE DATA               �   COPY public.geofences (geofenceid, geofencetype, addeddate, strockcolor, strockopacity, strockweight, fillcolor, fillopacity) FROM stdin;
    public       postgres    false    205   �O       T          0    16833    polygongeofence 
   TABLE DATA               N   COPY public.polygongeofence (id, geofenceid, latitude, longitude) FROM stdin;
    public       postgres    false    211   �P       R          0    16820    rectanglegeofence 
   TABLE DATA               U   COPY public.rectanglegeofence (id, geofenceid, north, east, west, south) FROM stdin;
    public       postgres    false    209   �P       L          0    16779    routehistory 
   TABLE DATA               �   COPY public.routehistory (routehistoryid, vehicleid, vehicledirection, status, vehiclespeed, epoch, address, latitude, longitude) FROM stdin;
    public       postgres    false    203   8Q       F          0    16670    vehicles 
   TABLE DATA               I   COPY public.vehicles (vehicleid, vehiclenumber, vehicletype) FROM stdin;
    public       postgres    false    197   MU       J          0    16692    vehiclesinformations 
   TABLE DATA               p   COPY public.vehiclesinformations (id, vehicleid, driverid, vehiclemake, vehiclemodel, purchasedate) FROM stdin;
    public       postgres    false    201   �U       c           0    0    circlegeofence_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.circlegeofence_id_seq', 3, true);
            public       postgres    false    206            d           0    0    driver_driverid_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.driver_driverid_seq', 7, true);
            public       postgres    false    198            e           0    0    geofences_geofenceid_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.geofences_geofenceid_seq', 3, true);
            public       postgres    false    204            f           0    0    polygongeofence_id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public.polygongeofence_id_seq', 3, true);
            public       postgres    false    210            g           0    0    rectanglegeofence_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.rectanglegeofence_id_seq', 2, true);
            public       postgres    false    208            h           0    0    routehistory_routehistoryid_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.routehistory_routehistoryid_seq', 125, true);
            public       postgres    false    202            i           0    0    vehicles_vehicleid_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.vehicles_vehicleid_seq', 11, true);
            public       postgres    false    196            j           0    0    vehiclesinformations_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public.vehiclesinformations_id_seq', 29, true);
            public       postgres    false    200            �
           2606    16812 "   circlegeofence circlegeofence_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.circlegeofence
    ADD CONSTRAINT circlegeofence_pkey PRIMARY KEY (id);
 L   ALTER TABLE ONLY public.circlegeofence DROP CONSTRAINT circlegeofence_pkey;
       public         postgres    false    207            �
           2606    16689    driver driver_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.driver
    ADD CONSTRAINT driver_pkey PRIMARY KEY (driverid);
 <   ALTER TABLE ONLY public.driver DROP CONSTRAINT driver_pkey;
       public         postgres    false    199            �
           2606    16804    geofences geofences_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.geofences
    ADD CONSTRAINT geofences_pkey PRIMARY KEY (geofenceid);
 B   ALTER TABLE ONLY public.geofences DROP CONSTRAINT geofences_pkey;
       public         postgres    false    205            �
           2606    16838 $   polygongeofence polygongeofence_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.polygongeofence
    ADD CONSTRAINT polygongeofence_pkey PRIMARY KEY (id);
 N   ALTER TABLE ONLY public.polygongeofence DROP CONSTRAINT polygongeofence_pkey;
       public         postgres    false    211            �
           2606    16825 (   rectanglegeofence rectanglegeofence_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.rectanglegeofence
    ADD CONSTRAINT rectanglegeofence_pkey PRIMARY KEY (id);
 R   ALTER TABLE ONLY public.rectanglegeofence DROP CONSTRAINT rectanglegeofence_pkey;
       public         postgres    false    209            �
           2606    16787    routehistory routehistory_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public.routehistory
    ADD CONSTRAINT routehistory_pkey PRIMARY KEY (routehistoryid);
 H   ALTER TABLE ONLY public.routehistory DROP CONSTRAINT routehistory_pkey;
       public         postgres    false    203            �
           2606    25040 %   vehiclesinformations vehicleid_unique 
   CONSTRAINT     e   ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehicleid_unique UNIQUE (vehicleid);
 O   ALTER TABLE ONLY public.vehiclesinformations DROP CONSTRAINT vehicleid_unique;
       public         postgres    false    201            �
           2606    16678    vehicles vehicles_pkey 
   CONSTRAINT     [   ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_pkey PRIMARY KEY (vehicleid);
 @   ALTER TABLE ONLY public.vehicles DROP CONSTRAINT vehicles_pkey;
       public         postgres    false    197            �
           2606    16700 .   vehiclesinformations vehiclesinformations_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_pkey PRIMARY KEY (id);
 X   ALTER TABLE ONLY public.vehiclesinformations DROP CONSTRAINT vehiclesinformations_pkey;
       public         postgres    false    201            �
           2606    16813 -   circlegeofence circlegeofence_geofenceid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.circlegeofence
    ADD CONSTRAINT circlegeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid);
 W   ALTER TABLE ONLY public.circlegeofence DROP CONSTRAINT circlegeofence_geofenceid_fkey;
       public       postgres    false    205    207    2751            �
           2606    16839 /   polygongeofence polygongeofence_geofenceid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.polygongeofence
    ADD CONSTRAINT polygongeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid);
 Y   ALTER TABLE ONLY public.polygongeofence DROP CONSTRAINT polygongeofence_geofenceid_fkey;
       public       postgres    false    2751    205    211            �
           2606    16826 3   rectanglegeofence rectanglegeofence_geofenceid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.rectanglegeofence
    ADD CONSTRAINT rectanglegeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid);
 ]   ALTER TABLE ONLY public.rectanglegeofence DROP CONSTRAINT rectanglegeofence_geofenceid_fkey;
       public       postgres    false    2751    205    209            �
           2606    16788 (   routehistory routehistory_vehicleid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.routehistory
    ADD CONSTRAINT routehistory_vehicleid_fkey FOREIGN KEY (vehicleid) REFERENCES public.vehicles(vehicleid);
 R   ALTER TABLE ONLY public.routehistory DROP CONSTRAINT routehistory_vehicleid_fkey;
       public       postgres    false    203    2741    197            �
           2606    16844 7   vehiclesinformations vehiclesinformations_driverid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_driverid_fkey FOREIGN KEY (driverid) REFERENCES public.driver(driverid) ON DELETE SET NULL;
 a   ALTER TABLE ONLY public.vehiclesinformations DROP CONSTRAINT vehiclesinformations_driverid_fkey;
       public       postgres    false    2743    199    201            �
           2606    16701 8   vehiclesinformations vehiclesinformations_vehicleid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_vehicleid_fkey FOREIGN KEY (vehicleid) REFERENCES public.vehicles(vehicleid);
 b   ALTER TABLE ONLY public.vehiclesinformations DROP CONSTRAINT vehiclesinformations_vehicleid_fkey;
       public       postgres    false    201    2741    197            P   K   x����@���%�zI�ud��6C�҃����f�{(� o祎0K<�Qyv$�8Sɚ����[��|>��      H   b   x��9�  ��W�,goebek�1$����N1�c�Ge2��`I���[G-O������H��+s���s����r
|U�=eP�����\���      N   �   x�M���0�g�a���d�3�S�%UDU��I�o��gk\?m��-���WM>�
<9�&R���.��Y�	���6y�D8��]r��Q6L���V�j�0;���Bm�R��`,k[�a�"��R�@P+�      T   ?   x�5˱�0��� ۠]��iH��a4�W�V{"�wh/"�ZZQC���u�t��P�u ��	      R   J   x�E˱�@C�v	�p�K��#�K�/�	�e>����,v~�zf�����,k�1E�/ثO��o}LU_�;�      L     x��YMo�F=/�~@L�|�|�4@	Z��^t#v��!��C��EA1�mӒ ��7��[�JP�Ը�) \��� �������û���Ï���x�?|.\{Tw(w�}��]�ÂcA��Q��|,�Mv�^��2W���0F�r`=2�[G�Fr6W�V]�XM�w�￾V{���*�[�j�=�s��/Z���f[mQ��Jh�v{�1;��Dnn���ŷ�j��������L-������k�_O�BoC�3�]ݼ��\-m.�M�V1f���Y�'zއ����Ե��;u>�^�+��˩[3��kJ4��ܭ)0��kR���n�� ���55oU#^	�59���51����55�-��Ѣ��{.q�- ��BQ�-�#�3�.�]U�F~g�;WcXV�ӷ*�^�{���KrP	���$�&�DP~����k� N��
2�c��H��r�q�B.m�x`+m�x�L���ji��m�Y<Ћl�Y�2�m�A8��&D���A\t�xP+�a<H&z�ă��� �m�i�&�V�nZ�By�?��_B�>��6�^Lّ;����tgn� �h���}<�����P�1b�.��f����~��qc�Ӑ��C.��?�c�k�Z���]P%�
��Sbg\\�;�bj&+c�WUT��L�v�O�%���{����B��^��/Fa#n@�B(�|7^A~yf@�q�2 ɀ4����]��
H2:IF'��$����'����%��X�:ɨ"U$��dT�LN5�S���5��fr�Gh��q�faGX��q�eaGX��q�eaGX��q�g�Gx��q�g�Gx��q�g5c�87�PSđ,���j�P3ƀ�qԌ5���AA�����4�0!"dNu��PKjܕ���H�K���`:_E܆x!�=�������S�cJ{Li��u/_���1�2�t��Z�(L���6�'�÷�V���t����oŎ�����w]���      F   e   x�-�9�0k�c�r�&55GE�"*��"(�=�{l���w�`(W��Cb2����ǉ�0KD�b�&+D5u��FP_^0Uӧ�~��q%�y_in���U      J   q   x�-���@C��1S|�KRڑ�jp�RaՓ�����S���( T~S˜U0������?�<w�i�s�`s�YH6ZU�t&��:RZ6Sݖa�Uj�!a�m,��"�aNG     