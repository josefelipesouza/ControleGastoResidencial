export function extractErrorMessage(err: any): string {
  const data = err?.response?.data;

  return (
    data?.errors?.[0]?.description ||
    data?.[0]?.description ||
    data?.detail ||
    data?.title ||
    'Erro inesperado ao processar a solicitação.'
  );
}
