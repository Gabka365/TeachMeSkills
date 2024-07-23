export default interface BoardGameViewModel {
    id: number;
    title: string;
    miniTitle: string;
    hasMainImage: boolean;
    hasSideImage: boolean;
    description: string;
    essence: string;
    tags: string;
    price: number;
    productCode: number;
    isFavoriteForUser: boolean;
}
