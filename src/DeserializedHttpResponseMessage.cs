using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   A tuple of a <see cref="HttpResponseMessage" />, a <see cref="Result{A}" /> of reading the
///   <see cref="HttpResponseMessage.Content" /> as a <see cref="string" />, and a <see cref="Result{A}" /> of
///   deserializing the <see cref="HttpResponseMessage.Content" /> as <typeparamref name="TBody" />.
/// </summary>
/// <param name="Message">The <see cref="HttpResponseMessage" /> which was parsed.</param>
/// <param name="Content">
///   A <see cref="Result{A}" /> of the attempt to read <paramref name="Message" /> as a
///   <see cref="string" />.
/// </param>
/// <param name="Body">
///   A <see cref="Result{A}" /> of the attempt to deserialize <paramref name="Content" /> as
///   <typeparamref name="TBody" />.
/// </param>
/// <typeparam name="TBody">A deserialization target type.</typeparam>
public record DeserializedHttpResponseMessage<TBody>(HttpResponseMessage Message, Result<string> Content,
  Result<TBody> Body);
