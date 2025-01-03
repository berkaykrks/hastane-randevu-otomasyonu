PGDMP         !                |            hastaneyonetimsistemi    14.15    14.15 .    !           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            "           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            #           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            $           1262    16459    hastaneyonetimsistemi    DATABASE     s   CREATE DATABASE hastaneyonetimsistemi WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Turkish_T�rkiye.1254';
 %   DROP DATABASE hastaneyonetimsistemi;
                postgres    false            �            1259    16538    tbl_branslar    TABLE     f   CREATE TABLE public.tbl_branslar (
    bransid integer NOT NULL,
    bransad character varying(30)
);
     DROP TABLE public.tbl_branslar;
       public         heap    postgres    false            �            1259    16537    tbl_branslar_bransid_seq    SEQUENCE     �   CREATE SEQUENCE public.tbl_branslar_bransid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.tbl_branslar_bransid_seq;
       public          postgres    false    210            %           0    0    tbl_branslar_bransid_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.tbl_branslar_bransid_seq OWNED BY public.tbl_branslar.bransid;
          public          postgres    false    209            �            1259    16545    tbl_doktorlar    TABLE     �   CREATE TABLE public.tbl_doktorlar (
    doktorid integer NOT NULL,
    doktorad character varying(20),
    doktorsoyad character varying(20),
    doktorbrans character varying(30),
    doktortc character(11),
    doktorsifre character varying(10)
);
 !   DROP TABLE public.tbl_doktorlar;
       public         heap    postgres    false            �            1259    16544    tbl_doktorlar_doktorid_seq    SEQUENCE     �   CREATE SEQUENCE public.tbl_doktorlar_doktorid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 1   DROP SEQUENCE public.tbl_doktorlar_doktorid_seq;
       public          postgres    false    212            &           0    0    tbl_doktorlar_doktorid_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public.tbl_doktorlar_doktorid_seq OWNED BY public.tbl_doktorlar.doktorid;
          public          postgres    false    211            �            1259    16552    tbl_duyurular    TABLE     h   CREATE TABLE public.tbl_duyurular (
    duyuruid integer NOT NULL,
    duyuru character varying(200)
);
 !   DROP TABLE public.tbl_duyurular;
       public         heap    postgres    false            �            1259    16551    tbl_duyurular_duyuruid_seq    SEQUENCE     �   CREATE SEQUENCE public.tbl_duyurular_duyuruid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 1   DROP SEQUENCE public.tbl_duyurular_duyuruid_seq;
       public          postgres    false    214            '           0    0    tbl_duyurular_duyuruid_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public.tbl_duyurular_duyuruid_seq OWNED BY public.tbl_duyurular.duyuruid;
          public          postgres    false    213            �            1259    16559    tbl_hastalar    TABLE     %  CREATE TABLE public.tbl_hastalar (
    hastaid integer NOT NULL,
    hastaad character varying(10),
    hastasoyad character varying(10),
    hastatc character varying(11),
    hastatelefon character varying(15),
    hastasifre character varying(10),
    hastacinsiyet character varying(5)
);
     DROP TABLE public.tbl_hastalar;
       public         heap    postgres    false            �            1259    16558    tbl_hastalar_hastaid_seq    SEQUENCE     �   CREATE SEQUENCE public.tbl_hastalar_hastaid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.tbl_hastalar_hastaid_seq;
       public          postgres    false    216            (           0    0    tbl_hastalar_hastaid_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.tbl_hastalar_hastaid_seq OWNED BY public.tbl_hastalar.hastaid;
          public          postgres    false    215            �            1259    16566    tbl_randevular    TABLE     R  CREATE TABLE public.tbl_randevular (
    randevuid integer NOT NULL,
    randevutarih character varying(10),
    randevusaat character varying(5),
    randevubrans character varying(30),
    randevudoktor character varying(20),
    randevudurum boolean DEFAULT false,
    hastatc character(11),
    hastasikayet character varying(250)
);
 "   DROP TABLE public.tbl_randevular;
       public         heap    postgres    false            �            1259    16565    tbl_randevular_randevuid_seq    SEQUENCE     �   CREATE SEQUENCE public.tbl_randevular_randevuid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE public.tbl_randevular_randevuid_seq;
       public          postgres    false    218            )           0    0    tbl_randevular_randevuid_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public.tbl_randevular_randevuid_seq OWNED BY public.tbl_randevular.randevuid;
          public          postgres    false    217            �            1259    16574    tbl_sekreter    TABLE     �   CREATE TABLE public.tbl_sekreter (
    sekreterid integer NOT NULL,
    sekreteradsoyad character varying(30),
    sekretertc character(11),
    sekretersifre character varying(10)
);
     DROP TABLE public.tbl_sekreter;
       public         heap    postgres    false            �            1259    16573    tbl_sekreter_sekreterid_seq    SEQUENCE     �   CREATE SEQUENCE public.tbl_sekreter_sekreterid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public.tbl_sekreter_sekreterid_seq;
       public          postgres    false    220            *           0    0    tbl_sekreter_sekreterid_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public.tbl_sekreter_sekreterid_seq OWNED BY public.tbl_sekreter.sekreterid;
          public          postgres    false    219            u           2604    16541    tbl_branslar bransid    DEFAULT     |   ALTER TABLE ONLY public.tbl_branslar ALTER COLUMN bransid SET DEFAULT nextval('public.tbl_branslar_bransid_seq'::regclass);
 C   ALTER TABLE public.tbl_branslar ALTER COLUMN bransid DROP DEFAULT;
       public          postgres    false    209    210    210            v           2604    16548    tbl_doktorlar doktorid    DEFAULT     �   ALTER TABLE ONLY public.tbl_doktorlar ALTER COLUMN doktorid SET DEFAULT nextval('public.tbl_doktorlar_doktorid_seq'::regclass);
 E   ALTER TABLE public.tbl_doktorlar ALTER COLUMN doktorid DROP DEFAULT;
       public          postgres    false    211    212    212            w           2604    16555    tbl_duyurular duyuruid    DEFAULT     �   ALTER TABLE ONLY public.tbl_duyurular ALTER COLUMN duyuruid SET DEFAULT nextval('public.tbl_duyurular_duyuruid_seq'::regclass);
 E   ALTER TABLE public.tbl_duyurular ALTER COLUMN duyuruid DROP DEFAULT;
       public          postgres    false    213    214    214            x           2604    16562    tbl_hastalar hastaid    DEFAULT     |   ALTER TABLE ONLY public.tbl_hastalar ALTER COLUMN hastaid SET DEFAULT nextval('public.tbl_hastalar_hastaid_seq'::regclass);
 C   ALTER TABLE public.tbl_hastalar ALTER COLUMN hastaid DROP DEFAULT;
       public          postgres    false    216    215    216            y           2604    16569    tbl_randevular randevuid    DEFAULT     �   ALTER TABLE ONLY public.tbl_randevular ALTER COLUMN randevuid SET DEFAULT nextval('public.tbl_randevular_randevuid_seq'::regclass);
 G   ALTER TABLE public.tbl_randevular ALTER COLUMN randevuid DROP DEFAULT;
       public          postgres    false    217    218    218            {           2604    16577    tbl_sekreter sekreterid    DEFAULT     �   ALTER TABLE ONLY public.tbl_sekreter ALTER COLUMN sekreterid SET DEFAULT nextval('public.tbl_sekreter_sekreterid_seq'::regclass);
 F   ALTER TABLE public.tbl_sekreter ALTER COLUMN sekreterid DROP DEFAULT;
       public          postgres    false    220    219    220                      0    16538    tbl_branslar 
   TABLE DATA           8   COPY public.tbl_branslar (bransid, bransad) FROM stdin;
    public          postgres    false    210   6                 0    16545    tbl_doktorlar 
   TABLE DATA           l   COPY public.tbl_doktorlar (doktorid, doktorad, doktorsoyad, doktorbrans, doktortc, doktorsifre) FROM stdin;
    public          postgres    false    212   �6                 0    16552    tbl_duyurular 
   TABLE DATA           9   COPY public.tbl_duyurular (duyuruid, duyuru) FROM stdin;
    public          postgres    false    214   
8                 0    16559    tbl_hastalar 
   TABLE DATA           v   COPY public.tbl_hastalar (hastaid, hastaad, hastasoyad, hastatc, hastatelefon, hastasifre, hastacinsiyet) FROM stdin;
    public          postgres    false    216   j8                 0    16566    tbl_randevular 
   TABLE DATA           �   COPY public.tbl_randevular (randevuid, randevutarih, randevusaat, randevubrans, randevudoktor, randevudurum, hastatc, hastasikayet) FROM stdin;
    public          postgres    false    218   �8                 0    16574    tbl_sekreter 
   TABLE DATA           ^   COPY public.tbl_sekreter (sekreterid, sekreteradsoyad, sekretertc, sekretersifre) FROM stdin;
    public          postgres    false    220   =       +           0    0    tbl_branslar_bransid_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.tbl_branslar_bransid_seq', 15, true);
          public          postgres    false    209            ,           0    0    tbl_doktorlar_doktorid_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.tbl_doktorlar_doktorid_seq', 14, true);
          public          postgres    false    211            -           0    0    tbl_duyurular_duyuruid_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.tbl_duyurular_duyuruid_seq', 3, true);
          public          postgres    false    213            .           0    0    tbl_hastalar_hastaid_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.tbl_hastalar_hastaid_seq', 3, true);
          public          postgres    false    215            /           0    0    tbl_randevular_randevuid_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public.tbl_randevular_randevuid_seq', 136, true);
          public          postgres    false    217            0           0    0    tbl_sekreter_sekreterid_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.tbl_sekreter_sekreterid_seq', 1, true);
          public          postgres    false    219            }           2606    16543    tbl_branslar tbl_branslar_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.tbl_branslar
    ADD CONSTRAINT tbl_branslar_pkey PRIMARY KEY (bransid);
 H   ALTER TABLE ONLY public.tbl_branslar DROP CONSTRAINT tbl_branslar_pkey;
       public            postgres    false    210                       2606    16550     tbl_doktorlar tbl_doktorlar_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.tbl_doktorlar
    ADD CONSTRAINT tbl_doktorlar_pkey PRIMARY KEY (doktorid);
 J   ALTER TABLE ONLY public.tbl_doktorlar DROP CONSTRAINT tbl_doktorlar_pkey;
       public            postgres    false    212            �           2606    16557     tbl_duyurular tbl_duyurular_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.tbl_duyurular
    ADD CONSTRAINT tbl_duyurular_pkey PRIMARY KEY (duyuruid);
 J   ALTER TABLE ONLY public.tbl_duyurular DROP CONSTRAINT tbl_duyurular_pkey;
       public            postgres    false    214            �           2606    16564    tbl_hastalar tbl_hastalar_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.tbl_hastalar
    ADD CONSTRAINT tbl_hastalar_pkey PRIMARY KEY (hastaid);
 H   ALTER TABLE ONLY public.tbl_hastalar DROP CONSTRAINT tbl_hastalar_pkey;
       public            postgres    false    216            �           2606    16572 "   tbl_randevular tbl_randevular_pkey 
   CONSTRAINT     g   ALTER TABLE ONLY public.tbl_randevular
    ADD CONSTRAINT tbl_randevular_pkey PRIMARY KEY (randevuid);
 L   ALTER TABLE ONLY public.tbl_randevular DROP CONSTRAINT tbl_randevular_pkey;
       public            postgres    false    218            �           2606    16579    tbl_sekreter tbl_sekreter_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.tbl_sekreter
    ADD CONSTRAINT tbl_sekreter_pkey PRIMARY KEY (sekreterid);
 H   ALTER TABLE ONLY public.tbl_sekreter DROP CONSTRAINT tbl_sekreter_pkey;
       public            postgres    false    220               �   x�]�;�0D��S���k!$$���Y�,Nl�	Hpΐ�`��3#�y=X�h���3XK�OF3�!7RQ���%5�8�M͖��0����%�*CkK���8Buh]��p?W��Pg�djD������6�M��{0r�;��z>$��Q�0s�[a��Pُ���QwD|��GT         8  x�e�MR1�םSx���o9(b���r�`�H���t����t隍���*�l��t:����L#��̄�2�O��V �7¦j"*���Kε���ٙ��m�;�Z�]�p�Ӝ�T���9Ǉ�h<�UW�rB���O1�"rr(���Tu��C[}a�����顲	=K3�E?�	�7�,ެe��}\��Gm!7v���)%���3��OJ��,�S���瞂�6P�Ĳq|����Uu�B)���B��<*�E���
�g����c@��6m��}�Ỉ
BD{»�9�<i�u�_�G�?��s��{��Q         P   x�3�t*M?�'O�(1/%��4'�H�81�D������Q�̔D��ģ�s�l��/��2��M-�HLJT�H-�N-N����� �	�         e   x�3�LL�LJ�4426153��44�� r4�\]SS3��wbʑ�y\Ɯ����%�.���E���f�&�F����PC83�KA�\��S��b���� 
�           x���Mn�6�t
] �D�gg'Nq�*��&�e$M@���]�e���3�W���R��U�Y���$?*r���|�/b'H^��s�e}�q�Re�]�R֟�s�x��sq�f�cH�"/����I}Y��r��p��T����ݕ��5��|�2P9��
|N��G�td���D�=,<��<K�8
E����X�V5E�}����/�.���&�@��{~���F��֭��Ҽ���|�]?��~��ת*�3�:�]7�o�pީ�3�d+��?��xo�Y6�p�Ȧ���o3�U/Q'~S�*�WJk���.3%g���=@⎼V����@N��j?��ݝ����!���o�}�[���w��yd݋D��u��kU7�Q�������[���{�<覔+��F����z�1����Z}�W�붷��>���u�*��<�jU��1��d�j���_��
��~�5�)��ݪ��Z�r�q�e�,�A��er�|��3&�er���d6!��>l"\&�����*4�¬'v�y��2)&�3��`J8���&�3�	�`8��7��fUi����A�A &�CF�D�aH�i ƁD�a 8��.��!�S����lsǹ9b��>!b!k����p�����|�Ƽ��8/�-cz?��!��ؙ#��5C��)CRk��|#�g���[@�-O(XS�;&4\g�I�^ڹ�^����\A�l��9��?C�?N���d���SOw����І�gm4� XO,�&�-�vTL�g��F��dw;*��2OB;*��B��[�i}�^�~��w���c}]/���-���}���A�G���>�vX�G��G���~o��㽱_By���\�}5�@`c�*�Y��5���f6׬��U���L��-F�U�!��F`�`֟X{F`��5�/b�����!�"Xu�DXv�`�)���V�"Xz�`�)�ŧ�}��^˘ƪ,Z5}������"��3+�,<#����3��,:#&.
܋�k�@^������6         )   x�3�J��K-�N�L�442�!����ԒT �+F��� ��
�     